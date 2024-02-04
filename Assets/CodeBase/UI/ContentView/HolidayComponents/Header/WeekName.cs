using CodeBase.Extensions;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.ContentView.HolidayComponents.Header
{
  public class WeekName : MonoBehaviour
  {
    public void SetWeekName(string text) => GetComponent<TextMeshProUGUI>().text = text.RemoveNewLineInBeginning();
  }
}