using CodeBase.Data.Services.DownloadServices;
using CodeBase.Infrastructure.Services;
using CodeBase.UI;

namespace CodeBase.Infrastructure.States
{
  public class DownloadingState : IState
  {
    private const string Main = "Main";
    
    private readonly ISceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IStateMover _resolver;
    private readonly IDownloadingService _downloadingService;

    public DownloadingState(
      ISceneLoader sceneLoader, LoadingCurtain curtain, IStateMover resolver, 
      IDownloadingService downloadingService
      )
    {
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _resolver = resolver;
      _downloadingService = downloadingService;
    }

    public void Enter()
    {
      _curtain.Show();
      _downloadingService.LoadHoliday(onLoaded: MoveToNextState);
    }

    public void Exit()
    {
    }

    private void MoveToNextState() => 
      _sceneLoader.LoadScene(Main, onLoaded: EnterLoadCalendarState);


    private void EnterLoadCalendarState() => 
      _resolver.MoveTo<LoadCalendarState>();
  }
}
