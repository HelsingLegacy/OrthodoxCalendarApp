using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup curtain;
    
    private readonly float _hideSpeed = .0015f;
    private IEnumerator _fading;
    
    public void Show()
    {
      gameObject.SetActive(true);
    }

    public void Hide()
    {
      _fading = Fading();
      StartCoroutine(_fading);
    }

    private IEnumerator Fading()
    {
      while (curtain.alpha > 0)
      {
        curtain.alpha -= 0.005f;
        yield return new WaitForSeconds(_hideSpeed);
      }

      gameObject.SetActive(false);
    }
  }
}