using CodeBase.Extensions;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.ContentHandlers.NonInteracting.HolidayComponents.Header
{
  public class WeekdayName : MonoBehaviour
  {
    public void SetWeekdayName(string text) => GetComponent<TextMeshProUGUI>().text = text.RemoveNewLineInBeginning();
  }
}