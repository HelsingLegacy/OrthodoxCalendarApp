using CodeBase.Data;
using CodeBase.Data.Services;
using CodeBase.Data.Services.JsonHandle;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.Infrastructure.States;
using CodeBase.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
  public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable, IStateMover
  {
    [SerializeField] private GameObject Curtain;
    
    private CalendarStateMachine _stateMachine;
    
    public override void InstallBindings()
    {
      BindBootstrapInstallerInterfaces();
      BindSceneLoader();
      BindLoadingCurtain(from: CurtainInstance());
      BindDataLoadingServices();
      BindFactory();
      BindCalendarStateMachine();
    }

    public void Initialize()
    {
      MoveTo<WarmUpState>();
    }

    public void MoveTo<TState>() where TState : IState => 
      Container.Resolve<CalendarStateMachine>().Enter<TState>();

    private void BindBootstrapInstallerInterfaces() => 
      Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();

    private void BindSceneLoader() => 
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

    private LoadingCurtain CurtainInstance() =>
      Container
        .InstantiatePrefabForComponent<LoadingCurtain>(Curtain);

    private void BindLoadingCurtain(LoadingCurtain from) => 
      Container.Bind<LoadingCurtain>().FromInstance(from).AsSingle();

    private void BindDataLoadingServices()
    {
      Container.Bind<ClearData>().AsSingle();
      Container.Bind<ILinkProvider>().To<LinkProvider>().AsTransient();
      Container.Bind<IKyivDate>().To<KyivDate>().AsTransient();
      Container.BindInterfacesTo<HolidaysStorage>().AsTransient();
      Container.Bind<IJsonSaver>().To<JsonSaver>().AsTransient();
      Container.Bind<IDownloadingService>().To<DownloadingService>().AsTransient();
    }

    private void BindFactory()
    {
      Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
      Container.Bind<CalendarFactory>().AsSingle();
    }

    private void BindCalendarStateMachine()
    {
      Container.Bind<UserObservationState>().AsSingle();
      Container.Bind<LoadCalendarState>().AsSingle();
      Container.Bind<WarmUpState>().AsSingle();
      Container.Bind<CalendarStateMachine>().AsSingle();
    }
  }
}