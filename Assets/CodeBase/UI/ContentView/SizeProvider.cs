using UnityEngine;

namespace CodeBase.UI.ContentView
{
  public class SizeProvider : MonoBehaviour
  {
    public float ObjectSize() => 
      GetComponent<RectTransform>().sizeDelta.y;
  }

}