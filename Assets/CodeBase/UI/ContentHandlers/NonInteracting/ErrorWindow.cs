using UnityEngine;

namespace CodeBase.UI.ContentHandlers.NonInteracting
{
  public class ErrorWindow : MonoBehaviour
  {
    public void SelfDestroy() => 
      Destroy(gameObject);
  }
}