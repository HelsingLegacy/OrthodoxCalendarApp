using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.UI.ContentView;
using UnityEngine;

namespace CodeBase.UI.Presenters
{
  public class HudMonthPresenter : MonoBehaviour
  {
    public MonthView View;
    
    private HudModel _model;
    private CalendarFactory _factory;
    
    public void Construct(HudModel model, CalendarFactory factory)
    {
      _model = model;
      _factory = factory;
    }

    public void SetMonthName(string month) =>
      View.SetGeneralMonth(month);

    public void ShowMonthList()
    {
      _model.ClearContent();
      _factory.CreateMonthList(_model.ContentPresenter.gameObject);
    }
  }
}