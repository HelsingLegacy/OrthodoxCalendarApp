using System;
using System.Collections.Generic;
using CodeBase.Extensions;

namespace CodeBase.Infrastructure.Services.TimeDate
{
  public class KyivDate : IKyivDate, IToday, IMonthName
  {
    public List<string> DaysFor(Month month, string year)
    {
      List<string> allDays = new List<string>();

      int days = DateTime.DaysInMonth(year.ToInteger(), (int)month);

      for (int day = 1; day <= days; day++)
      {
        DateTime date = new DateTime(year.ToInteger(), (int)month, day);
        
        allDays.Add(date.ToStringDateFormat());
      }
      
      return allDays;
    }
    
    public DateTime TodayKyivDate() => 
      DateTime.UtcNow.AddHours(SummerTimeOffsetAdjustment(accordingTo: DateTime.UtcNow));
    
    public string TodayKyivText() => 
      DateTime.UtcNow
        .AddHours(SummerTimeOffsetAdjustment(accordingTo: DateTime.UtcNow))
        .ToStringDateFormat();

    public string CurrentMonth()
    {
      switch (TodayKyivDate().Month)
      {
        case 1:
          return "Січень";
        case 2:
          return "Лютий";
        case 3:
          return "Березень";
        case 4:
          return "Квітень";
        case 5:
          return "Травень";
        case 6:
          return "Червень";
        case 7:
          return "Липень";
        case 8:
          return "Серпень";
        case 9:
          return "Вересень";
        case 10:
          return "Жовтень";
        case 11:
          return "Листопад";
        case 12:
          return "Грудень";
      }

      return "Error";
    }

    private int SummerTimeOffsetAdjustment(DateTime accordingTo)
    {
      if (accordingTo >= LastMarchSunday(accordingTo) && accordingTo <= LastOctoberSunday(accordingTo))
        return 3;

      return 2;
    }

    private DateTime LastMarchSunday(DateTime utcNow)
    {
      DateTime lastMarchDay = new DateTime(utcNow.Year, 3, 31);
      DateTime lastMarchSunday = lastMarchDay.AddDays(-(int)lastMarchDay.DayOfWeek);
      return lastMarchSunday;
    }

    private DateTime LastOctoberSunday(DateTime utcNow)
    {
      DateTime lastOctoberDay = new DateTime(utcNow.Year, 10, 31);
      DateTime lastOctoberSunday = lastOctoberDay.AddDays(-(int)lastOctoberDay.DayOfWeek);
      return lastOctoberSunday;
    }
  }
}