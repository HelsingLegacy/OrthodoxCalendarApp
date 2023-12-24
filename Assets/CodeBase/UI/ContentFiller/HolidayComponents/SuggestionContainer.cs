using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentFiller.HolidayComponents
{
  public class SuggestionContainer : MonoBehaviour
  {
    public void SetSuggestions(List<Sprite> sprites)
    {
      var images = new List<Image>(GetComponentsInChildren<Image>());

      for (int i = 0; i < images.Count; i++)
        images[i].sprite = sprites[i];
    }
  }
}