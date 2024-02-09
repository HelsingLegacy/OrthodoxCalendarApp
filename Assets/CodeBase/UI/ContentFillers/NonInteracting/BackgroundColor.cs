using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentFillers.NonInteracting
{
  public class BackgroundColor : MonoBehaviour
  {
    public void SetBackground(Color color) => 
      GetComponent<Image>().color = color;
  }
}