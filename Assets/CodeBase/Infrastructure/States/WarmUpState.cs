using CodeBase.Data.Services;
using CodeBase.Infrastructure.Services;
using CodeBase.UI;

namespace CodeBase.Infrastructure.States
{
  public class WarmUpState : IState
  {
    private const string Main = "Main";
    
    private readonly ISceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IStateMover _resolver;
    private readonly IDownloadingService _downloadingService;
    private readonly IHolidaysStorageCreator _holidaysStorageCreator;

    public WarmUpState(ISceneLoader sceneLoader, LoadingCurtain curtain, IStateMover resolver, IDownloadingService downloadingService, IHolidaysStorageCreator holidaysStorageCreator)
    {
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _resolver = resolver;
      _downloadingService = downloadingService;
      _holidaysStorageCreator = holidaysStorageCreator;
    }

    public void Enter()
    {
      _curtain.Show();
      _holidaysStorageCreator.CreateFolderJsonData();
      _downloadingService.LoadHolidays();
      _sceneLoader.LoadScene(Main, EnterLoadCalendar);
    }

    public void Exit()
    {
    }

    private void EnterLoadCalendar() => 
      _resolver.MoveTo<LoadCalendarState>();
  }
}
