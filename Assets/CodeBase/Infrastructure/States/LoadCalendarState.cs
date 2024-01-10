using CodeBase.Extensions;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.TimeDate;
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
    private readonly IToday _today;

    public LoadCalendarState(LoadingCurtain curtain, CalendarFactory factory, IStateMover resolver, IToday today)
    {
      _curtain = curtain;
      _factory = factory;
      _resolver = resolver;
      _today = today;
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
          .GetComponent<ParentProvider>()
          .ParentObject();

      GameObject monthParent =
        _factory
          .CreateMonthContainer(under: hud.transform)
          .GetComponent<ParentProvider>()
          .ParentObject();

        _factory.CreateHolidayDataAssembly(monthParent.transform, _today.TodayKyiv().ToStringDateFormat()); 
        //"2024-01-07"
        //_today.TodayKyiv().ToStringDateFormat()
    }
  }
}