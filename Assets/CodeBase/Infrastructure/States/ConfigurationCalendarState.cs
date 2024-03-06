using CodeBase.Infrastructure.Services;
using CodeBase.UI;
using CodeBase.UI.Mediator;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class CalendarAssemblyState : IState
  {
    private readonly LoadingCurtain _curtain;
    private readonly CalendarFactory _factory;
    private readonly IStateMover _stateMover;

    public CalendarAssemblyState(LoadingCurtain curtain, CalendarFactory factory, IStateMover stateMover)
    {
      _curtain = curtain;
      _factory = factory;
      _stateMover = stateMover;
    }

    public void Enter()
    {
      InitCalendar();
      
      _stateMover.MoveTo<UserObservationState>();
      
      _curtain.Hide();
    }

    public void Exit()
    {
    }

    private void InitCalendar()
    {
     GameObject hud = HudInitialization();
     
     hud.GetComponent<Shifting>().ShiftMediatorParent();
     
     hud.GetComponent<MainWindow>().ShowTodayHoliday();
    }

    private GameObject HudInitialization() => 
      _factory.CreateHudWithBinding();
  }
}