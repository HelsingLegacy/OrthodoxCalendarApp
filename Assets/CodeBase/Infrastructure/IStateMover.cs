using CodeBase.Data.Services;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
  public interface IStateMover : IService
  {
    void MoveTo<TState>() where TState : IState;
  }
}