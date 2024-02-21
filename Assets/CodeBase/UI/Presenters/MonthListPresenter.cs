using CodeBase.Data.Services.DownloadServices;
using CodeBase.Data.Services.HolidayObserverService;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI.Mediator;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Presenters
{
  public class MonthListPresenter : MonoBehaviour
  {
    private HudMediator _mediator;
    private LoadingCurtain _curtain;
    private CalendarFactory _factory;
    private IKyivDate _dateService;
    private string _year;
    private IDownloadingService _downloadingService;
    private IHolidayDataObserver _holidayDataObserver;

    [Inject]
    public void Construct(HudMediator mediator, LoadingCurtain curtain, CalendarFactory factory, IKyivDate dateService,
      IDownloadingService downloadingService, IHolidayDataObserver holidayDataObserver)
    {
      _mediator = mediator;
      _curtain = curtain;
      _factory = factory;
      _dateService = dateService;
      _year = _mediator.GetCurrentYear();
      _downloadingService = downloadingService;
      _holidayDataObserver = holidayDataObserver;
    }

    public async UniTask ShowOrDownload(Month month)
    {
      _curtain.Show();
      _mediator.ResetAndCleanupContent();

      if (!_holidayDataObserver.Has(month, _year))
      {
        await _downloadingService.DownloadHolidays(month, _year);
      }

      ShowShortHolidaysList(month, _year);

      _curtain.HideWithDelay();
    }

    private void ShowShortHolidaysList(Month month, string year)
    {
      foreach (string day in _dateService.DaysFor(month, year))
        _factory.CreateHolidayShortInfo(_mediator.ContentContainer, day);
    }
  }
}