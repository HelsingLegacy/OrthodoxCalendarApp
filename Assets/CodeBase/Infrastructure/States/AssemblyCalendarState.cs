using CodeBase.Infrastructure.Services;
using CodeBase.UI;

namespace CodeBase.Infrastructure.States
{
  public class AssemblyCalendarState : IState
  {
    private readonly LoadingCurtain _curtain;
    private readonly CalendarFactory _factory;
    private readonly IStateMover _stateMover;

    public AssemblyCalendarState(LoadingCurtain curtain, CalendarFactory factory, IStateMover stateMover)
    {
      _curtain = curtain;
      _factory = factory;
      _stateMover = stateMover;
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
      HudInitialization();

      _stateMover.MoveTo<UserObservationState>();
    }

    private void HudInitialization() => 
      _factory.CreateHud();
  }
}