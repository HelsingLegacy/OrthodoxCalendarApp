using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentHandlers.NonInteracting
{
  public class ProgressBar : MonoBehaviour
  {
    public Image Bar;

    private float _particle;

    public void SetBarValues(int size)
    {
      _particle = 1f / size;
      Bar.fillAmount = 0f;
    }

    public void UpdateProgress() => 
      Bar.fillAmount += _particle;

    public void SelfDestruction() => 
      Destroy(gameObject);
  }
}