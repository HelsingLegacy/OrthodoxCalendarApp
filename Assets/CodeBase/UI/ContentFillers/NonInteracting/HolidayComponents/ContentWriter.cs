using CodeBase.Extensions;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.ContentFillers.NonInteracting.HolidayComponents
{
  public class ContentWriter : MonoBehaviour
  {
    public void SetContent(string text) => 
      GetComponentInChildren<TextMeshProUGUI>().text = text.RemoveNewLineInBeginning();
  }
}