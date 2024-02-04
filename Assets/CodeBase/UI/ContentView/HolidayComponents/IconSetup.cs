using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentView.HolidayComponents
{
  public class IconSetup : MonoBehaviour
  {
    public void SetIcon(Sprite sprite) => GetComponent<Image>().sprite = sprite;
  }
}