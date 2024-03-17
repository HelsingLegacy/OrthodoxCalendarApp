using CodeBase.Extensions;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.ContentHandlers.NonInteracting.HolidayComponents.Header
{
  public class WeekName : MonoBehaviour
  {
    public void SetWeekName(string text) => GetComponent<TextMeshProUGUI>().text = text.RemoveNewLineInBeginning();
  }
}