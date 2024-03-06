using CodeBase.Data.Services.AssetProviding;
using CodeBase.Data.Services.DownloadServices;
using CodeBase.Data.Services.HolidayObserverService;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.Infrastructure.States;
using CodeBase.UI;
using Zenject;

namespace CodeBase.Infrastructure
{
  public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable, IStateMover
  {
    private CalendarStateMachine _stateMachine;
    
    public override void InstallBindings()
    {
      BindLoadingCurtain();
      BindBootstrapInstallerInterfaces();
      BindSceneLoader();
      BindSupportServices();
      BindDataLoadingServices();
      BindFactory();
      BindCalendarStateMachine();
    }

    public void Initialize() => 
      MoveTo<DownloadingState>();

    public void MoveTo<TState>() where TState : IState => 
      Container.Resolve<CalendarStateMachine>().Enter<TState>();

    private void BindBootstrapInstallerInterfaces() => 
      Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();

    private void BindSceneLoader() => 
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

    private void BindLoadingCurtain() => 
      Container.Bind<LoadingCurtain>()
        .FromInstance(FindAnyObjectByType<LoadingCurtain>())
        .AsSingle();

    private void BindDataLoadingServices()
    {
      Container.Bind<ILinkProvider>().To<LinkProvider>().AsSingle();
      Container.Bind<ILoadingDataService>().To<LoadingDataService>().AsSingle();
      Container.Bind<IDownloadingService>().To<DownloadingService>().AsSingle();
    }

    private void BindSupportServices()
    {
      Container.BindInterfacesTo<KyivDate>().AsSingle();
      Container.BindInterfacesTo<HolidaysStorageFolder>().AsSingle();
      
      Container.Bind<IConfigProvider>().To<ConfigProvider>().AsSingle();
      Container.Bind<IHolidayDataObserver>().To<HolidayDataObserver>().AsSingle();
    }

    private void BindFactory()
    {
      Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
      Container.Bind<CalendarFactory>().AsSingle();
    }

    private void BindCalendarStateMachine()
    {
      Container.Bind<UserObservationState>().AsSingle();
      Container.Bind<CalendarAssemblyState>().AsSingle();
      Container.Bind<DownloadingState>().AsSingle();
      Container.Bind<CalendarStateMachine>().AsSingle();
    }
  }
}