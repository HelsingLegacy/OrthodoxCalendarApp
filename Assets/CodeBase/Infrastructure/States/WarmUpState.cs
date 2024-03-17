using CodeBase.Data.Services.AssetProviding;
using CodeBase.Infrastructure.Services.ErrorHandling;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.UI;

namespace CodeBase.Infrastructure.States
{
  public class WarmUpState : IState
  {
    private const string Download = "Download";
    private readonly IStorageConfiguration _storageConfig;
    private readonly LoadingCurtain _curtain;
    private readonly ISceneLoader _sceneLoader;
    private readonly IStateMover _stateMover;
    private readonly IErrorSaver _error;

    public WarmUpState(LoadingCurtain curtain, IStorageConfiguration storageConfig,
      ISceneLoader sceneLoader, IStateMover stateMover, IErrorSaver error)
    {
      _curtain = curtain;
      _storageConfig = storageConfig;
      _sceneLoader = sceneLoader;
      _stateMover = stateMover;
      _error = error;
    }

    public void Enter()
    {
      _curtain.Show();
      
      PrepareStorage();
      ResetError();

      _sceneLoader.LoadScene(Download, MoveToNextState);
    }

    private void ResetError() => 
      _error.SetErrorCode(ErrorID.NoErrors);

    public void Exit()
    {
    }

    private void MoveToNextState() =>
      _stateMover.MoveTo<DownloadingState>();

    private void PrepareStorage()
    {
      _storageConfig.BindDataPath();
      _storageConfig.CreateDataFolders();
    }

    
  }
}