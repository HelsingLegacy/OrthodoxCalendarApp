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
    private readonly IHolidayDataObserver _holidayDataObserver;
    private readonly IKyivDate _dateService;

    public DownloadingService(IDataLoaderService dataLoader, IHolidayDataObserver holidayDataObserver, 
      IKyivDate dateService)
    {
      _dataLoader = dataLoader;
      _holidayDataObserver = holidayDataObserver;
      _dateService = dateService;
    }

    public async UniTask DownloadHoliday(string date, Action onLoaded = null)
    {
      int progress = 0;
      float progressIcons = 0;

      if (!_holidayDataObserver.RequestedFileExistFor(date))
      {
        int process = await _dataLoader.LoadRawHoliday(date);
        float processIcons = await _dataLoader.LoadIcons(date);

        progress += process;
        progressIcons += processIcons;
      }

      if ((progress > 0 && Mathf.Abs(progressIcons - 1f) < 0.07f) || _holidayDataObserver.RequestedFileExistFor(date))
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