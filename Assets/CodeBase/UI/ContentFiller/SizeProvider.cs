using UnityEngine;

namespace CodeBase.UI.ContentFiller
{
  public class SizeProvider : MonoBehaviour
  {
    public float ObjectSize() => 
      GetComponent<RectTransform>().sizeDelta.y;
  }

}