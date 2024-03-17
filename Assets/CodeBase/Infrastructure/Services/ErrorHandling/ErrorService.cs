using System;
using System.Collections;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.States;
using CodeBase.UI.ContentHandlers.NonInteracting;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.ErrorHandling
{
  public class ErrorService : IErrorSaver, IErrorStateProvider, IErrorHandler
  {
    private const string Error = "Error";

    private bool _isErrorPoppedUp;

    private readonly ISceneLoader _sceneLoader;
    private readonly IStateMover _stateMover;
    private readonly IErrorFactory _errorFactory;

    private ErrorWindow _errorWindow;

    private ErrorID Code { get; set; }

    public ErrorService(ISceneLoader sceneLoader, IStateMover stateMover, IErrorFactory errorFactory)
    {
      _sceneLoader = sceneLoader;
      _stateMover = stateMover;
      _errorFactory = errorFactory;
    }

    public void SetErrorCode(ErrorID id)
    {
      if (Code == ErrorID.NoErrors) 
        Code = id;
    }

    public bool IsAnError() => 
      Code != ErrorID.NoErrors;

    public bool IsNoError() => 
      Code == ErrorID.NoErrors;

    public void PopupError()
    {
      if (_isErrorPoppedUp)
        return;

      _isErrorPoppedUp = true;

      _sceneLoader.LoadScene(Error, onLoaded: EnterObservationState);
      ErrorShow();
    }

    public IEnumerator ResetError(Action onLoaded = null)
    {
      SetErrorCode(ErrorID.NoErrors);
      ErrorHide();
      _isErrorPoppedUp = false;
      yield return new WaitForSeconds(1f);
      
      onLoaded?.Invoke();
    }

    private void ErrorHide() => 
      _errorWindow.SelfDestroy();

    private void ErrorShow() => 
      _errorWindow = _errorFactory.CreateErrorWindow().GetComponent<ErrorWindow>();

    private void EnterObservationState() =>
      _stateMover.MoveTo<ObservationState>();
  }
}