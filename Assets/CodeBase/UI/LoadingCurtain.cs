using System.Collections;
using UnityEngine;

namespace CodeBase.UI
{
  public class LoadingCurtain : MonoBehaviour
  {
    public CanvasGroup curtain;
    
    private readonly float _hideSpeed = .0015f;

    public void Show() => 
      gameObject.SetActive(true);

    public void Hide() => 
      StartCoroutine(Fading());

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