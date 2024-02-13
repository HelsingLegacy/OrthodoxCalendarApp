using TMPro;
using UnityEngine;

namespace CodeBase.UI.ContentFillers.NonInteracting
{
  public class MonthText : MonoBehaviour
  {
    public void SetGeneralMonth(string text) => 
      GetComponentInChildren<TextMeshProUGUI>().text = text;
  }
}