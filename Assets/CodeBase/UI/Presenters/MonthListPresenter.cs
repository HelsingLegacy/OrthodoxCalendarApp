using System.Collections.Generic;
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

    [Inject]
    public void Construct(HudMediator mediator, CalendarFactory factory)
    {
      _mediator = mediator;
      _factory = factory;
    }

    public void Show(Month month)
    {
      if (_mediator.Has(month))
      {
        _mediator.ClearContent();
        ShowShortHolidaysList();
      }
    }

    private void ShowShortHolidaysList()
    {
      List<string> days = new();
      
      days.Add("2024-02-04");
      days.Add("2024-02-07");
      days.Add("2024-02-08");
      days.Add("2024-02-09");
      
      foreach (var day in days)
      {
        _factory.CreateHolidayShortInfo(_mediator.ContentContainer, day);
      }
    }
  }
}