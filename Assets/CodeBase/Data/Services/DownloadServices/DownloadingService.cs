using System;
using CodeBase.Data.Services.HolidayObserverService;
using CodeBase.Infrastructure.Services.ErrorHandling;
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

    public DownloadingService(ILoadingDataService loadingData, IHolidayDataObserver dataObserver,
      IKyivDate dateService, IErrorSaver errorSaver, IErrorStateProvider errorProvider)
    {
      _loadingData = loadingData;
      _dataObserver = dataObserver;
      _dateService = dateService;
      _errorSaver = errorSaver;
      _errorProvider = errorProvider;
    }

    public async UniTask DownloadHoliday(string date, Action onLoaded = null)
    {
      if (_errorProvider.IsAnError())
        return;
      
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
      
      foreach (string day in _dateService.DaysFor(month, year))
      {
        if (_errorProvider.IsAnError())
          return;

        await DownloadHoliday(day);
      }
    }
  }
}