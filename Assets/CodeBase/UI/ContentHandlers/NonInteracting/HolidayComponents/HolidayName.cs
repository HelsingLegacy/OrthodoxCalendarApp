using CodeBase.Extensions;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.ContentHandlers.NonInteracting.HolidayComponents
{
  public class HolidayName : MonoBehaviour
  {
    public void SetHolidayName(string text) => GetComponentInChildren<TextMeshProUGUI>().text = text.RemoveNewLineInBeginning();
  }
}