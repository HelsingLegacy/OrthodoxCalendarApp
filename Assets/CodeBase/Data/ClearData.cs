using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data
{
  public class ClearData
  {
    public Color HeaderColor;
    public string WeekdayName;
    public string DateMonth;

    public bool IsWeekNameEmpty;
    public string WeekName;

    public bool IsShortView;
    public Sprite MainIcon;

    public bool IsHolidayNameEmpty;
    public string HolidayName;

    public List<Sprite> Suggestions;

    public string ShortContentText;

    public bool IsAnyDayIcons;
    public List<Sprite> DayIcons;


    public ClearData(Color headerColor, string weekdayName, string dateMonth,
      bool isWeekNameEmpty, string weekName, bool isShortView, Sprite mainIcon,
      bool isHolidayNameEmpty, string holidayName, List<Sprite> suggestions, 
      string shortContentText, bool isAnyDayIcons, List<Sprite> dayIcons)
    {
      HeaderColor = headerColor;
      WeekdayName = weekdayName;
      DateMonth = dateMonth;
      IsWeekNameEmpty = isWeekNameEmpty;
      WeekName = weekName;
      IsShortView = isShortView;
      MainIcon = mainIcon;
      IsHolidayNameEmpty = isHolidayNameEmpty;
      HolidayName = holidayName;
      Suggestions = suggestions;
      ShortContentText = shortContentText;
      IsAnyDayIcons = isAnyDayIcons;
      DayIcons = dayIcons;
    }
  }
}