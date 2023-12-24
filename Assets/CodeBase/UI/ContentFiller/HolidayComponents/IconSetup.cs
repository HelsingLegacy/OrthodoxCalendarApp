using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentFiller.HolidayComponents
{
  public class IconSetup : MonoBehaviour
  {
    public void SetIcon(Sprite sprite) => GetComponent<Image>().sprite = sprite;
  }
}