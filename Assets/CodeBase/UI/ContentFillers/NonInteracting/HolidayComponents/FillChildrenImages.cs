using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentFillers.NonInteracting.HolidayComponents
{
  public class FillChildrenImages : MonoBehaviour
  {
    public void SetImagesWith(List<Sprite> sprites)
    {
      var images = new List<Image>(GetComponentsInChildren<Image>());

      for (int i = 0; i < images.Count; i++)
        images[i].sprite = sprites[i];
    }
  }
}