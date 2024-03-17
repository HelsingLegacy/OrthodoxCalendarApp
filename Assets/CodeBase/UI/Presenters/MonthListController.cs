using CodeBase.Data.Services.DownloadServices;
using CodeBase.Data.Services.HolidayObserverService;
using CodeBase.Infrastructure.Services.ErrorHandling;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.ScriptableData;
using CodeBase.UI.ContentHandlers.Interacting;
using CodeBase.UI.Mediator;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Presenters
{
  public class MonthListController : MonoBehaviour
  {
    private MainWindow _mediator;
    private LoadingCurtain _curtain;
    private CalendarFactory _factory;
    private IKyivDate _dateService;
    private string _year;
    private IDownloadingService _downloadingService;
    private IHolidayDataObserver _dataObserver;
    private IErrorStateProvider _errorStateProvider;
    private IErrorHandler _errorHandler;
    private IToday _today;

    [Inject]
    public void Construct(MainWindow mediator, LoadingCurtain curtain, CalendarFactory factory, 
      IKyivDate dateService, IDownloadingService downloadingService, 
      IHolidayDataObserver holidayDataObserver, IErrorStateProvider errorStateProvider, 
      IErrorHandler errorHandler, IToday today)
    {
      _mediator = mediator;
      _curtain = curtain;
      _factory = factory;
      _dateService = dateService;
      _year = _mediator.GetCurrentYear();
      _downloadingService = downloadingService;
      _dataObserver = holidayDataObserver;
      _errorStateProvider = errorStateProvider;
      _errorHandler = errorHandler;
      _today = today;
    }

    public void ActivateButtons()
    {
      MonthButton[] buttons = GetComponentsInChildren<MonthButton>();

      var colorData = LoadScriptableData();
      
      foreach (MonthButton button in buttons)
      {
        button.SetColor(_today.IsTwentyOf(button.Month) 
          ? colorData.Available 
          : colorData.Unavailable);
      }
    }

    public async UniTask ShowOrDownload(Month month)
    {
      _mediator.ResetAndCleanupContent();

      if (!_dataObserver.Has(month, _year)) 
        await _downloadingService.DownloadHolidays(month, _year);

      if (_errorStateProvider.IsAnError())
      {
        _errorHandler.PopupError();
        return;
      }
      
      ShowShortHolidaysList(month, _year);

      _curtain.HideWithDelay();
    }

    private void ShowShortHolidaysList(Month month, string year)
    {
      foreach (string day in _dateService.DaysFor(month, year))
        _factory.CreateHolidayShortInfo(_mediator.ContentContainer, day);
    }

    private MonthListColorsData LoadScriptableData() => 
      Resources.Load<MonthListColorsData>("ScriptableData/MonthButtonStateColor");
  }
}