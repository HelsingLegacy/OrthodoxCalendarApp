using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class UserObservationState : IState
  {
    public void Enter()
    {
      Debug.Log("ObservationState");
    }

    public void Exit()
    {
    }
  }
}