using System.Collections;
using UnityEngine;

namespace CodeBase.UI
{
  public class LoadingCurtain : MonoBehaviour
  {
    [SerializeField] private CanvasGroup _curtain;
    public float Delay;

    [SerializeField] private GameObject _errorPopup;

    private void Awake() => 
      DontDestroyOnLoad(gameObject);

    public void PopupError() => 
      _errorPopup.SetActive(true);

    public void ErrorHide() => 
      _errorPopup.SetActive(false);

    public void Show()
    {
      _curtain.alpha = 1;
      gameObject.SetActive(true);
    }

    public void Hide() => 
      StartCoroutine(Fading());

    public void HideWithDelay() => 
      StartCoroutine(Fading(Delay));

    private IEnumerator Fading(float delay = 0)
    {
      if (delay > 0)
        yield return new WaitForSeconds(delay);
        
      while (_curtain.alpha > 0)
      {
        _curtain.alpha -= 0.035f;
        yield return null;
      }

      gameObject.SetActive(false);
    }
  }
}