using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data
{
  public class ClearData
  {
    public Color HeaderColor { get; }
    public string WeekdayName { get; }
    public string DateMonth { get; }
    
    public bool IsWeekNameEmpty { get; }
    public string WeekName { get; }

    public Sprite MainIcon { get; }
    public string HolidayName { get; }

    public List<Sprite> Suggestions { get; }
    
    public string ShortContentText { get; }
    
    public Sprite DayIcons { get; }
  }
}