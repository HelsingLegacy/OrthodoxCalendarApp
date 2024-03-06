using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.States
{
  public class CalendarStateMachine
  {
    private readonly Dictionary<Type, IState> _states;
    private IState _activeState;
    
    public CalendarStateMachine(DownloadingState downloadingState, CalendarAssemblyState calendarAssemblyState,
                                UserObservationState userObservationState) 
    {
      _states = new Dictionary<Type, IState>
      {
        [typeof(DownloadingState)] = downloadingState,
        [typeof(CalendarAssemblyState)] = calendarAssemblyState,
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