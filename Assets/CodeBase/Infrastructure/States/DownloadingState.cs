using CodeBase.Data.Services.DownloadServices;
using CodeBase.Extensions;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI;

namespace CodeBase.Infrastructure.States
{
  public class DownloadingState : IState
  {
    private const string Main = "Main";
    
    private readonly ISceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IStateMover _stateMover;
    private readonly IDownloadingService _downloadingService;
    private readonly IToday _today;

    public DownloadingState(
      ISceneLoader sceneLoader, LoadingCurtain curtain, IStateMover stateMover, 
      IDownloadingService downloadingService, IToday today)
    {
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _stateMover = stateMover;
      _downloadingService = downloadingService;
      _today = today;
    }

    public void Enter()
    {
      _curtain.Show();
      _downloadingService.LoadHoliday(_today.TodayKyiv().ToStringDateFormat(), onLoaded: MoveToNextState);
    }

    public void Exit()
    {
    }

    private void MoveToNextState() => 
      _sceneLoader.LoadScene(Main, onLoaded: EnterLoadCalendarState);


    private void EnterLoadCalendarState() => 
      _stateMover.MoveTo<AssemblyCalendarState>();
  }
}
