using System;
using CodeBase.Data.Services.HolidayObserverService;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Data.Services.DownloadServices
{
  public class DownloadingService : IDownloadingService
  {
    private readonly ILoadingDataService _loadingData;
    private readonly IHolidayDataObserver _holidayDataObserver;
    private readonly IKyivDate _dateService;
    private readonly LoadingCurtain _curtain;

    public DownloadingService(ILoadingDataService loadingData, IHolidayDataObserver holidayDataObserver, 
      IKyivDate dateService, LoadingCurtain curtain)
    {
      _loadingData = loadingData;
      _holidayDataObserver = holidayDataObserver;
      _dateService = dateService;
      _curtain = curtain;
    }

    public async UniTask DownloadHoliday(string date, Action onLoaded = null)
    {
      bool jsonExistFor = _holidayDataObserver.JsonExistFor(date);
      if (!jsonExistFor) 
        await _loadingData.LoadRawHoliday(date);

      bool iconsExistFor = _holidayDataObserver.IconsExistFor(date);
      if(!iconsExistFor) 
        await _loadingData.LoadIcons(date); 
      
      if (jsonExistFor && iconsExistFor)
        onLoaded?.Invoke();
      else
      {
        Debug.LogError($"Cannot download config and/or icons for {date}.");
        _curtain.PopupError();
      }
    }

    public async UniTask DownloadHolidays(Month month, string year)
    {
      foreach (string day in _dateService.DaysFor(month, year))
        await DownloadHoliday(day);

    }
  }
}