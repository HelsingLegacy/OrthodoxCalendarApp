using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
  public interface IStateMover
  {
    void MoveTo<TState>() where TState : IState;
  }
}