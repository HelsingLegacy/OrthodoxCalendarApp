using System.Collections.Generic;
using System.IO;
using CodeBase.Data.Services.AssetProviding;
using CodeBase.Extensions;
using CodeBase.Infrastructure.Services.TimeDate;

namespace CodeBase.Data.Services.HolidayObserverService
{
  class HolidayDataObserver : IHolidayDataObserver
  {
    private readonly IHolidaysStorage _holidaysStorage;
    private readonly IKyivDate _dateService;

    public HolidayDataObserver(IHolidaysStorage holidaysStorage, IKyivDate dateService)
    {
      _holidaysStorage = holidaysStorage;
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
      File.Exists(_holidaysStorage.HolidayConfigFor(date));

    public bool IconsExistFor(string date)
    {
      int iconsForDate = 1;
      
      var icons = new ClearIconsLinks(_holidaysStorage, date);

      for(int i = 1; i<= icons.DayIcons.Count; i++)
      {
        if(string.IsNullOrEmpty(_holidaysStorage.HolidayIconFor(date.WithIndex(i))))
          continue;
        
        iconsForDate++;
      }
      
      if (iconsForDate == icons.DayIcons.Count + 1)
        return true;
      return false;
    }
  }
}