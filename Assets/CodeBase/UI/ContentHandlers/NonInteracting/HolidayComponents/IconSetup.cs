using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentHandlers.NonInteracting.HolidayComponents
{
  public class IconSetup : MonoBehaviour
  {
    public void SetIcon(Sprite sprite) => GetComponent<Image>().sprite = sprite;
  }
}