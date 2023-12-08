using CodeBase.Infrastructure.Services;
using CodeBase.UI;
using CodeBase.UI.ContentFiller;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LoadCalendarState : IState
  {
    private readonly LoadingCurtain _curtain;
    private readonly CalendarFactory _factory;
    private readonly IStateMover _resolver;

    public LoadCalendarState(LoadingCurtain curtain, CalendarFactory factory, IStateMover resolver)
    {
      _curtain = curtain;
      _factory = factory;
      _resolver = resolver;
    }

    public void Enter()
    {
      InitCalendar();
      _curtain.Hide();
    }

    private void InitCalendar()
    {
      GameObject hud = _factory.CreateHud().GetComponent<MonthParent>().ParentObject();
      GameObject monthParent = _factory.CreateMonthContainer(under: hud.transform).GetComponent<DaysParent>().ParentObject();
      _factory.CreateDayData(under: monthParent.transform);
      _factory.CreateDayData(under: monthParent.transform);
      _factory.CreateDayData(under: monthParent.transform);
      _factory.CreateDayData(under: monthParent.transform);
      
      _resolver.MoveTo<UserObservationState>();
    }

    public void Exit()
    {
      
    }
  }
}