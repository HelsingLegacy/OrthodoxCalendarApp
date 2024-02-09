using UnityEngine;

namespace CodeBase.UI.ContentFillers.NonInteracting
{
  public class SizeProvider : MonoBehaviour
  {
    public float ObjectSize() => 
      GetComponent<RectTransform>().sizeDelta.y;
  }

}