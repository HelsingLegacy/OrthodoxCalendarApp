using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class HudMediator : MonoBehaviour
  {
    
    public void ShiftMediatorParent()
    {
      HudParent parent = FindObjectOfType<HudParent>();
      
      gameObject.GetComponent<RectTransform>().SetParent(parent.RectParent());
      gameObject.GetComponent<RectTransform>().SetParent(new RectTransform());
      
      Destroy(parent.gameObject);
    }
  }
}