﻿using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentView.HolidayComponents.Header
{
  public class HolidayHeader : MonoBehaviour
  {
    public void SetBackground(Color color) => GetComponent<Image>().color = color;
    public void SetWeekdayName(string text) => GetComponentInChildren<WeekdayName>().SetWeekdayName(text);
    public void SetDateMonth(string text) => GetComponentInChildren<DateAndMonth>().SetDateAndMonth(text);
  }
}