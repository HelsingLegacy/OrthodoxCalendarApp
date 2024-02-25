using UnityEngine;

namespace CodeBase.UI.Mediator
{
  public class Shifting : MonoBehaviour
  {
    public void ShiftMediatorParent()
    {
      HudParent parent = FindAnyObjectByType<HudParent>();
      
      gameObject.GetComponent<RectTransform>().SetParent(parent.RectParent());
      gameObject.GetComponent<RectTransform>().SetParent(new RectTransform());

      Destroy(parent.gameObject);
      Destroy(gameObject.GetComponent<Shifting>());
    }
  }
}