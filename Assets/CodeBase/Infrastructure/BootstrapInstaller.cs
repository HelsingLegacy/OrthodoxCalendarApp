using CodeBase.Data.Services;
using CodeBase.Data.Services.AssetProviding;
using CodeBase.Data.Services.DownloadServices;
using CodeBase.Data.Services.HolidayObserverService;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.ErrorHandling;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.Infrastructure.States;
using CodeBase.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
  public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable, IStateMover
  {
    public LoadingCurtain _curtain;
    
    private CalendarStateMachine _stateMachine;

    public override void InstallBindings()
    {
      BindLoadingCurtain();
      BindBootstrapInstallerInterfaces();
      BindSceneLoader();
      BindSupportServices();
      BindDataLoadingServices();
      BindFactory();
      BindErrorHandleServices();
      BindCalendarStateMachine();
    }

    public void Initialize() => 
      MoveTo<WarmUpState>();

    public void MoveTo<TState>() where TState : IState => 
      Container.Resolve<CalendarStateMachine>().Enter<TState>();

    private void BindLoadingCurtain()
    {
      LoadingCurtain curtain = Instantiate(_curtain);
      Container.Bind<LoadingCurtain>()
        .FromInstance(curtain)
        .AsSingle();
    }

    private void BindBootstrapInstallerInterfaces() => 
      Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();

    private void BindSceneLoader() => 
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

    private void BindSupportServices()
    {
      Container.BindInterfacesTo<KyivDate>().AsSingle();
      Container.BindInterfacesTo<HolidaysDataStorage>().AsSingle();
      
      Container.Bind<IConfigProvider>().To<ConfigProvider>().AsSingle();
      Container.Bind<IHolidayDataObserver>().To<HolidayDataObserver>().AsSingle();
    }

    private void BindDataLoadingServices()
    {
      Container.Bind<ILinkProvider>().To<LinkProvider>().AsSingle();
      Container.Bind<ILoadingDataService>().To<LoadingDataService>().AsSingle();
      Container.Bind<IDownloadingService>().To<DownloadingService>().AsSingle();
    }

    private void BindFactory()
    {
      Container.BindInterfacesTo<AssetProvider>().AsSingle();
      Container.Bind<CalendarFactory>().AsSingle();
      Container.Bind<IErrorFactory>().To<ErrorFactory>().AsSingle();
    }

    private void BindErrorHandleServices()
    {
      Container.Bind<IRestartService>().To<RestartService>().AsSingle();
      Container.BindInterfacesTo<ErrorService>().AsSingle();
    }

    private void BindCalendarStateMachine()
    {
      Container.Bind<ObservationState>().AsSingle();
      Container.Bind<CalendarConfigurationState>().AsSingle();
      Container.Bind<DownloadingState>().AsSingle();
      Container.Bind<WarmUpState>().AsSingle();
      Container.Bind<CalendarStateMachine>().AsSingle();
    }
  }
}