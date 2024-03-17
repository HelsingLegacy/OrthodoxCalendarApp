using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.States
{
  public class CalendarStateMachine
  {
    private readonly Dictionary<Type, IState> _states;
    private IState _activeState;
    
    public CalendarStateMachine(WarmUpState warmUpState, DownloadingState downloadingState, CalendarConfigurationState calendarConfigurationState,
                                ObservationState observationState) 
    {
      _states = new Dictionary<Type, IState>
      {
        [typeof(WarmUpState)] = warmUpState,
        [typeof(DownloadingState)] = downloadingState,
        [typeof(CalendarConfigurationState)] = calendarConfigurationState,
        [typeof(ObservationState)] = observationState,
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