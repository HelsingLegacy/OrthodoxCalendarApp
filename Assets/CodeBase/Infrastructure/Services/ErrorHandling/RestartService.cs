using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure.Services.ErrorHandling
{
  public class RestartService : IRestartService
  {
    private readonly IStateMover _stateMover;
    private readonly IErrorHandler _error;
    private readonly ICoroutineRunner _coroutineRunner;

    public RestartService(IStateMover stateMover, IErrorHandler error, ICoroutineRunner coroutineRunner)
    {
      _stateMover = stateMover;
      _error = error;
      _coroutineRunner = coroutineRunner;
    }

    public void RestartApplication()
    {
      _coroutineRunner.StartCoroutine(_error.ResetError(onLoaded: MoveToWarmUpState));
    }
    
    private void MoveToWarmUpState() => 
      _stateMover.MoveTo<WarmUpState>();
  }
}