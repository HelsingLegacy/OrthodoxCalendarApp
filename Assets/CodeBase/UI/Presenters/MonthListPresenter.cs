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
    private CalendarFactory _factory;
    private IKyivDate _dateService;
    private string _year;
    private IDownloadingService _downloadingService;
    private IHolidayObserver _holidayObserver;

    [Inject]
    public void Construct(HudMediator mediator, CalendarFactory factory, IKyivDate dateService,
      IDownloadingService downloadingService, IHolidayObserver holidayObserver)
    {
      _mediator = mediator;
      _factory = factory;
      _dateService = dateService;
      _year = _mediator.GetCurrentYear();
      _downloadingService = downloadingService;
      _holidayObserver = holidayObserver;
    }

    public async UniTask ShowOrDownload(Month month)
    {
      if (_holidayObserver.Has(month, _year))
      {
        _mediator.ClearContent();
        ShowShortHolidaysList(month, _year);
      }
      else
      {
        _mediator.ClearContent();
        _factory.CreateNoticePopup(_mediator.ContentContainer);

        await _downloadingService.DownloadHolidays(month, _year);
        
        _mediator.ClearContent();
        ShowShortHolidaysList(month, _year);
      }
    }

    private void ShowShortHolidaysList(Month month, string year)
    {
      foreach (string day in _dateService.DaysFor(month, year)) 
        _factory.CreateHolidayShortInfo(_mediator.ContentContainer, day);
    }
  }
}