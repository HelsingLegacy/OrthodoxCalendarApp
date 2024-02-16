using CodeBase.Data.Services.DownloadServices;
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

    public async UniTask ShowOrDownload(Month month)
    {
      // if (!_mediator.Has(month, _year))
      // {
      //   _mediator.ClearContent();
      //   ShowShortHolidaysList(month, _year);
      // }
      // else
      // {
        
        await DownloadHolidays(month, _year);
      // }
    }

    private void ShowShortHolidaysList(Month month, string year)
    {
      foreach (string day in _dateService.DaysFor(month, year))
      {
        _factory.CreateHolidayShortInfo(_mediator.ContentContainer, day);
      }
    }

    private async UniTask DownloadHolidays(Month month, string year)
    {
      _mediator.ClearContent();
      GameObject popup = _factory.CreateNoticePopup(_mediator.ContentContainer);
      
      foreach (string day in _dateService.DaysFor(month, year)) 
        await _downloadingService.LoadHoliday(day);
      
      ShowShortHolidaysList(month, _year);
      
      Destroy(popup);
    }
  }
}