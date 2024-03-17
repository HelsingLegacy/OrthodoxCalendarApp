using System.Collections.Generic;
using System.IO;
using CodeBase.Data.Services.AssetProviding;
using CodeBase.Extensions;
using CodeBase.Infrastructure.Services.TimeDate;

namespace CodeBase.Data.Services.HolidayObserverService
{
  class HolidayDataObserver : IHolidayDataObserver
  {
    private readonly IHolidaysDataStorage _holidaysDataStorage;
    private readonly IKyivDate _dateService;

    public HolidayDataObserver(IHolidaysDataStorage holidaysDataStorage, IKyivDate dateService)
    {
      _holidaysDataStorage = holidaysDataStorage;
      _dateService = dateService;
    }

    public bool Has(Month month, string year)
    {
      List<string> days = _dateService.DaysFor(month, year);
      int missingHolidays = days.Count;

      foreach (string day in days)
      {
        if (JsonExistFor(day) && IconsExistFor(day))
          missingHolidays--;
      }

      if (missingHolidays > 0)
        return false;
      
      return true;
    }
    
    public bool JsonExistFor(string date) =>
      File.Exists(_holidaysDataStorage.HolidayConfigFor(date));

    public bool IconsExistFor(string date)
    {
      if (!JsonExistFor(date))
        return false;
      
      int iconsForDate = 0;
      
      var icons = new ClearIconsLinks(_holidaysDataStorage, date);

      if (File.Exists(_holidaysDataStorage.HolidayIconFor(date)))
        iconsForDate++;
      
      for(int i = 1; i<= icons.DayIcons.Count; i++)
      {
        if(!File.Exists(_holidaysDataStorage.HolidayIconFor(date.WithIndex(i))))
          continue;        
        
        iconsForDate++;
      }
      
      if (iconsForDate == icons.DayIcons.Count + 1)
        return true;
      return false;
    }
  }
}