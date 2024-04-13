using System;
using System.Collections.Generic;
using CodeBase.Data.Services.HolidayObserverService;
using CodeBase.Infrastructure.Services.ErrorHandling;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.TimeDate;
using Cysharp.Threading.Tasks;

namespace CodeBase.Data.Services.DownloadServices
{
  public class DownloadingService : IDownloadingService
  {
    private readonly ILoadingDataService _loadingData;
    private readonly IHolidayDataObserver _dataObserver;
    private readonly IKyivDate _dateService;
    private readonly IErrorSaver _errorSaver;
    private readonly IErrorStateProvider _errorProvider;
    private readonly IProgressFactory _progressFactory;

    public DownloadingService(ILoadingDataService loadingData, IHolidayDataObserver dataObserver,
      IKyivDate dateService, IErrorSaver errorSaver, IErrorStateProvider errorProvider, IProgressFactory progressFactory)
    {
      _loadingData = loadingData;
      _dataObserver = dataObserver;
      _dateService = dateService;
      _errorSaver = errorSaver;
      _errorProvider = errorProvider;
      _progressFactory = progressFactory;
    }

    public async UniTask DownloadHoliday(string date, Action onLoaded = null)
    {
      if (_errorProvider.IsAnError())
      {
        onLoaded?.Invoke();
        return;
      }      
      
      if (!_dataObserver.JsonExistFor(date) && _errorProvider.IsNoError())
        await _loadingData.LoadRawHoliday(date);

      if (_dataObserver.JsonExistFor(date)
          && !_dataObserver.IconsExistFor(date) && _errorProvider.IsNoError())
        await _loadingData.LoadIcons(date);

      if (!_dataObserver.JsonExistFor(date) || !_dataObserver.IconsExistFor(date))
        _errorSaver.SetErrorCode(ErrorID.ReadingFailure);

      onLoaded?.Invoke();
    }

    public async UniTask DownloadHolidays(Month month, string year)
    {
      if (_errorProvider.IsAnError())
        return;

      List<string> daysFor = _dateService.DaysFor(month, year);

      var bar = _progressFactory.CreateProgress(daysFor.Count);

      foreach (string day in daysFor)
      {
        if (_errorProvider.IsAnError())
          return;

        await DownloadHoliday(day);
        bar.UpdateProgress();
      }

      bar.SelfDestruction();
    }
  }
}