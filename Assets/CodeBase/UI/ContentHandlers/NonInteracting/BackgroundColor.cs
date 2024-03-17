using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentHandlers.NonInteracting
{
  public class BackgroundColor : MonoBehaviour
  {
    public void SetBackground(Color color) => 
      GetComponent<Image>().color = color;
  }
}