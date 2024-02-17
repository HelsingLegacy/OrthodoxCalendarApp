using System;
using CodeBase.Data.Services.HolidayObserverService;
using CodeBase.Infrastructure.Services.TimeDate;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Data.Services.DownloadServices
{
  public class DownloadingService : IDownloadingService
  {
    private readonly IDataLoaderService _dataLoader;
    private readonly IHolidayObserver _holidayObserver;
    private readonly IKyivDate _dateService;

    public DownloadingService(IDataLoaderService dataLoader, IHolidayObserver holidayObserver, 
      IKyivDate dateService)
    {
      _dataLoader = dataLoader;
      _holidayObserver = holidayObserver;
      _dateService = dateService;
    }

    public async UniTask DownloadHoliday(string date, Action onLoaded = null)
    {
      int progress = 0;
      float progressIcons = 0;

      if (!_holidayObserver.RequestedFileExistFor(date))
      {
        int process = await _dataLoader.LoadRawHoliday(date);
        float processIcons = await _dataLoader.LoadIcons(date);

        progress += process;
        progressIcons += processIcons;
      }

      if ((progress > 0 && Mathf.Abs(progressIcons - 1f) < 0.07f) || _holidayObserver.RequestedFileExistFor(date))
        onLoaded?.Invoke();
      else
      {
        Debug.LogError($"Cannot download config and/or icons for {date}.");
      }
    }

    public async UniTask DownloadHolidays(Month month, string year)
    {
      foreach (string day in _dateService.DaysFor(month, year))
        await DownloadHoliday(day);

    }
  }
}