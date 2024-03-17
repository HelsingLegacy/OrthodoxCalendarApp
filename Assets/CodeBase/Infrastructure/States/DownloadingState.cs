using System;
using CodeBase.Data.Services.DownloadServices;
using CodeBase.Infrastructure.Services.ErrorHandling;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Services.TimeDate;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
  public class DownloadingState : IState
  {
    private const string Main = "Main";

    private readonly ISceneLoader _sceneLoader;
    private readonly IStateMover _stateMover;
    private readonly IDownloadingService _downloadingService;
    private readonly IToday _today;
    private readonly IErrorStateProvider _errorStateProvide;
    private readonly IErrorHandler _errorHandler;

    public DownloadingState(
      ISceneLoader sceneLoader, IStateMover stateMover,
      IDownloadingService downloadingService, IToday today, IErrorStateProvider errorStateProvide,
      IErrorHandler errorHandler)
    {
      _sceneLoader = sceneLoader;
      _stateMover = stateMover;
      _downloadingService = downloadingService;
      _today = today;
      _errorStateProvide = errorStateProvide;
      _errorHandler = errorHandler;
    }

    public async void Enter()
    {
      await DownloadTodayHoliday(onLoaded: LoadNextState);
    }

    public void Exit()
    {
    }

    private void LoadNextState()
    {
      if (_errorStateProvide.IsNoError())
        _sceneLoader.LoadScene(Main,
          onLoaded: EnterLoadCalendarState);
      else
        _errorHandler.PopupError();
    }

    private async UniTask DownloadTodayHoliday(Action onLoaded) =>
      await _downloadingService
        .DownloadHoliday(_today.TodayKyivText(),
          onLoaded);

    private void EnterLoadCalendarState() =>
      _stateMover.MoveTo<CalendarConfigurationState>();
  }
}