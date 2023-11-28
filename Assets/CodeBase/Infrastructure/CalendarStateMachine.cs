using System;
using System.Collections.Generic;
using Zenject;

namespace CodeBase.Infrastructure
{
  public class CalendarStateMachine
  {
    private readonly Dictionary<Type,IState> _states;
    private IState _activeState;

    [Inject]
    public CalendarStateMachine(BootstrapState bootstrapState, LoadCalendarState loadCalendarState)
    {
      _states = new Dictionary<Type, IState>
      {
        [typeof(BootstrapState)] = bootstrapState,
        [typeof(LoadCalendarState)] = loadCalendarState, 
      };
    }
    
    public void Enter<TState>() where TState : IState
    {
      _activeState?.Exit();
      IState state = _states[typeof(TState)];
      state.Enter();
    }
  }
}