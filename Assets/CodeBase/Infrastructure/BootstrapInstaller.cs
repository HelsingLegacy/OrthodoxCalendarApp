using System;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
  public class BootstrapInstaller : MonoInstaller
  {
    [SerializeField] private GameObject Curtain;

    public override void InstallBindings()
    {
      LoadingCurtain curtain = Container
        .InstantiatePrefabForComponent<LoadingCurtain>(Curtain);

      Container
        .Bind<LoadingCurtain>()
        .FromInstance(curtain)
        .AsSingle();
      
      curtain.Show();
      curtain.Hide();

      Container
        .Bind<CalendarUOC>()
        .AsSingle();
      
      Container
        .Bind<CalendarStateMachine>()
        .AsSingle();
      
      Container
        .Bind<BootstrapState>()
        .AsSingle();
      
      Container
        .Bind<LoadCalendarState>()
        .AsSingle();
    }
  }
}