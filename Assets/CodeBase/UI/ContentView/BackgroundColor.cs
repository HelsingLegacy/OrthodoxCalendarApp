using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentView
{
  public class BackgroundColor : MonoBehaviour
  {
    public void SetBackground(Color color) => 
      GetComponent<Image>().color = color;
  }
}