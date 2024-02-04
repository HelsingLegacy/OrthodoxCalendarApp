using TMPro;
using UnityEngine;

namespace CodeBase.UI.ContentView
{
  public class MonthView : MonoBehaviour
  {
    public void SetGeneralMonth(string text) => 
      GetComponentInChildren<TextMeshProUGUI>().text = text;
  }
}