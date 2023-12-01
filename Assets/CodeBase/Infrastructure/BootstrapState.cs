using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class BootstrapState : IState
  {
    public BootstrapState()
    {
      Debug.Log("Hello Zenject");
    }
    
    public void Enter()
    {
    }

    public void Exit()
    {
    }
  }
}