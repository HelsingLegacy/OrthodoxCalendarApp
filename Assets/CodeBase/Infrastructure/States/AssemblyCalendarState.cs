using CodeBase.Extensions;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI;
using CodeBase.UI.ContentFiller;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class AssemblyCalendarState : IState
  {
    private readonly LoadingCurtain _curtain;
    private readonly CalendarFactory _factory;
    private readonly IStateMover _stateMover;
    private readonly IToday _today;

    public AssemblyCalendarState(LoadingCurtain curtain, CalendarFactory factory, IStateMover stateMover, IToday today)
    {
      _curtain = curtain;
      _factory = factory;
      _stateMover = stateMover;
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
      DayAssembly();

      _stateMover.MoveTo<UserObservationState>();
    }

    private void DayAssembly()
    {
      GameObject hud =
        _factory
          .CreateHud()
          .GetComponent<ParentProvider>()
          .ParentObject();

      GameObject monthParent =
        _factory
          .CreateMonthContainer(under: hud)
          .GetComponent<ParentProvider>()
          .ParentObject();

        _factory.CreateHolidayFullInfo(monthParent, _today.TodayKyiv().ToStringDateFormat()); 
        //"2024-01-07"
        //_today.TodayKyiv().ToStringDateFormat()
    }
  }
}