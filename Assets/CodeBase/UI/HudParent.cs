using UnityEngine;

namespace CodeBase.UI
{
  public class HudParent : MonoBehaviour
  {
    public RectTransform RectParent() => 
      GetComponent<RectTransform>();
  }
}
