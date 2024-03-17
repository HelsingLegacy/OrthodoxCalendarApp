using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class ObservationState : IState
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