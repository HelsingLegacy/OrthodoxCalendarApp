﻿using CodeBase.Extensions;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.ContentFiller.HolidayComponents.Header
{
  public class DateAndMonth : MonoBehaviour
  {
    public void SetDateAndMonth(string text) => GetComponent<TextMeshProUGUI>().text = text.RemoveNewLineInBeginning();
  }
}