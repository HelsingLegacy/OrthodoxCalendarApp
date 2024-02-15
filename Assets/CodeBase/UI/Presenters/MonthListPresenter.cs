using CodeBase.Data.Services.DownloadServices;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI.Mediator;
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

    [Inject]
    public void Construct(HudMediator mediator, CalendarFactory factory, IKyivDate dateService,
      IDownloadingService downloadingService)
    {
      _mediator = mediator;
      _factory = factory;
      _dateService = dateService;
      _year = _mediator.GetCurrentYear();
      _downloadingService = downloadingService;
    }

    public void ShowOrDownload(Month month)
    {
      if (_mediator.Has(month, _year))
      {
        _mediator.ClearContent();
        ShowShortHolidaysList(month, _year);
      }
      else
      {
        _factory.CreateNoticePopup();
      }
    }

    private void ShowShortHolidaysList(Month month, string year)
    {
      // foreach (string day in _dateService.DaysFor(month, year))
      // {
      //   _downloadingService.LoadHoliday(day);
      // }
      
      foreach (string day in _dateService.DaysFor(month, year))
      {
        _factory.CreateHolidayShortInfo(_mediator.ContentContainer, day);
      }
    }
  }
}