using Zenject;

namespace CodeBase.Infrastructure
{
  public class CalendarUOC
  {
    private readonly CalendarStateMachine _stateMachine;

    [Inject]
    public CalendarUOC(CalendarStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }
  }
}