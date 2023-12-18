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

    public void Exit()
    {
    }

    private void InitCalendar()
    {
      TestUIElementsCreation();

      _resolver.MoveTo<UserObservationState>();
    }

    private void TestUIElementsCreation()
    {
      GameObject hud =
        _factory
          .CreateHud()
          .GetComponent<MonthParent>()
          .ParentObject();

      GameObject monthParent =
        _factory
          .CreateMonthContainer(under: hud.transform)
          .GetComponent<DaysParent>()
          .ParentObject();

      _factory.CreateHolidayDataRed(under: monthParent.transform);
      _factory.CreateHolidayDataBlack(under: monthParent.transform);
      _factory.CreateHolidayDataBlack(under: monthParent.transform);
      _factory.CreateHolidayDataRed(under: monthParent.transform);
    }
  }
}