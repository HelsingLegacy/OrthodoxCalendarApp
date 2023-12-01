using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure
{
  public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
  {
    [SerializeField] private GameObject Curtain;
    
    private CalendarStateMachine _stateMachine;

    public override void InstallBindings()
    {
      Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
      
      LoadingCurtain curtain = Container
        .InstantiatePrefabForComponent<LoadingCurtain>(Curtain);

      Container.Bind<LoadingCurtain>().FromInstance(curtain).AsSingle();
      
      curtain.Show();
      curtain.Hide();

      Container.Bind<CalendarUOC>().AsSingle();
      
      Container.Bind<CalendarStateMachine>().AsSingle();
      
      Container.Bind<BootstrapState>().AsSingle();
      
      Container.Bind<LoadCalendarState>().AsSingle();
    }


    public void Initialize()
    {
      Container.Resolve<ISceneLoader>().LoadScene("Main");
    }
  }
}