using CodeBase.Infrastructure.Services.ErrorHandling;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.UI.ContentHandlers.Interacting
{
  public class ErrorButton : MonoBehaviour, IPointerClickHandler
  {
    [SerializeField] private ButtonId _buttonId;

    private IRestartService _restarter;

    [Inject]
    public void Construct(IRestartService restarter) =>
      _restarter = restarter;

    public void OnPointerClick(PointerEventData eventData)
    {
      Debug.Log("Clicked");
      switch (_buttonId)
      {
        case ButtonId.Restart:
          _restarter.RestartApplication();
          break;
        case ButtonId.Quit:
          Application.Quit();
          break;
      }
    }
  }
}