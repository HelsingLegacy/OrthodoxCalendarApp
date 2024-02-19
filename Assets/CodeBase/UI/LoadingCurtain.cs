using System.Collections;
using UnityEngine;

namespace CodeBase.UI
{
  public class LoadingCurtain : MonoBehaviour
  {
    [SerializeField] private CanvasGroup _curtain;
    public float Delay;

    private readonly float _hideSpeed = .0015f;

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
        _curtain.alpha -= 0.005f;
        yield return new WaitForSeconds(_hideSpeed);
      }

      gameObject.SetActive(false);
    }
  }
}