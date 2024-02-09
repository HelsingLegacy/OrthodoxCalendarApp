using CodeBase.Infrastructure.Services;
using CodeBase.UI.ContentFillers.NonInteracting;
using CodeBase.UI.Mediator;
using UnityEngine;

namespace CodeBase.UI.Presenters
{
  public class HudMonthPresenter : MonoBehaviour
  {
    public MonthView View;
    
    private HudMediator _mediator;
    private CalendarFactory _factory;
    
    public void Construct(HudMediator mediator, CalendarFactory factory)
    {
      _mediator = mediator;
      _factory = factory;
    }

    public void SetMonthName(string month) =>
      View.SetGeneralMonth(month);

    public void ShowMonthList()
    {
      _mediator.ClearContent();
      _factory.CreateMonthList(_mediator);
    }
  }
}