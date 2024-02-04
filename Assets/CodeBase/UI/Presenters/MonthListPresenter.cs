using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.UI.Presenters
{
  public class MonthListPresenter : MonoBehaviour
  {
    private HudModel _model;
    private CalendarFactory _factory;

    public void Construct(HudModel model, CalendarFactory factory)
    {
      _model = model;
      _factory = factory;
    }

    public void Show(Month month)
    {
      if (_model.Has(month))
      {
        _model.ContentPresenter.CleanUp();
        ShowShortHolidaysList();
      }
    }

    private void ShowShortHolidaysList()
    {
      List<string> days = new();
      
      days.Add("2024-02-04");
      days.Add("2024-02-05");
      days.Add("2024-02-06");
      days.Add("2024-02-07");
      
      foreach (var day in days)
      {
        _factory.CreateHolidayShortInfo(_model.ContentPresenter.gameObject, day);
      }
    }
  }
}