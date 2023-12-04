using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.States
{
  public class CalendarStateMachine
  {
    private readonly Dictionary<Type, IState> _states;
    private IState _activeState;
    
    public CalendarStateMachine(WarmUpState warmUpState, LoadCalendarState loadCalendarState,
                                UserObservationState userObservationState) 
    {
      _states = new Dictionary<Type, IState>
      {
        [typeof(WarmUpState)] = warmUpState,
        [typeof(LoadCalendarState)] = loadCalendarState,
        [typeof(UserObservationState)] = userObservationState,
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