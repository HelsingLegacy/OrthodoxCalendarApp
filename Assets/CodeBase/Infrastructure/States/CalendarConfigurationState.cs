using CodeBase.Infrastructure.Services.Factory;
using CodeBase.UI;
using CodeBase.UI.Mediator;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class CalendarConfigurationState : IState
  {
    private readonly LoadingCurtain _curtain;
    private readonly CalendarFactory _factory;
    private readonly IStateMover _stateMover;

    public CalendarConfigurationState(LoadingCurtain curtain, CalendarFactory factory, IStateMover stateMover)
    {
      _curtain = curtain;
      _factory = factory;
      _stateMover = stateMover;
    }

    public void Enter()
    {
      InitCalendar();
      
      _stateMover.MoveTo<ObservationState>();
      
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