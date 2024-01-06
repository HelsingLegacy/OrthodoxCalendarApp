using CodeBase.Data.Services;
using CodeBase.Data.Services.AssetProviding;
using CodeBase.Data.Services.DownloadServices;
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
    private readonly IHolidaysStorageFolderCreator _holidaysStorageFolderCreator;

    public WarmUpState(ISceneLoader sceneLoader, LoadingCurtain curtain, IStateMover resolver, IDownloadingService downloadingService, IHolidaysStorageFolderCreator holidaysStorageFolderCreator)
    {
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _resolver = resolver;
      _downloadingService = downloadingService;
      _holidaysStorageFolderCreator = holidaysStorageFolderCreator;
    }

    public void Enter()
    {
      _curtain.Show();
      _holidaysStorageFolderCreator.CreateFolderJsonData();
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
