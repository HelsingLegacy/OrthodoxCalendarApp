using System.Collections.Generic;
using System.IO;
using CodeBase.Data.Services.AssetProviding;
using CodeBase.Infrastructure.Services.TimeDate;

namespace CodeBase.Data.Services.HolidayObserverService
{
  class HolidayObserver : IHolidayObserver
  {
    private readonly IHolidaysStorage _holidaysStorage;
    private readonly IKyivDate _dateService;

    public HolidayObserver(IHolidaysStorage holidaysStorage, IKyivDate dateService)
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
        if (RequestedFileExistFor(day))
          missingHolidays--;
      }

      if (missingHolidays > 0)
        return false;
      
      return true;
    }
    
    public bool RequestedFileExistFor(string date) =>
      File.Exists(_holidaysStorage.HolidayConfigFor(date));

  }
}