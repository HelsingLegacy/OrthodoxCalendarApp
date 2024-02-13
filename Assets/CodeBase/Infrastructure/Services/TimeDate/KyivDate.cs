﻿using System;
using CodeBase.Extensions;

namespace CodeBase.Infrastructure.Services.TimeDate
{
  public class KyivDate : IKyivDate
  {
    private DateTime MinusWeekFromToday => TodayKyivDate().AddDays(-3);
    private DateTime PlusWeekFromToday => TodayKyivDate().AddDays(3);

    public DateTime StartDate() => MinusWeekFromToday;

    public DateTime EndDate() => PlusWeekFromToday;

    public DateTime TodayKyivDate() => 
      DateTime.UtcNow.AddHours(SummerTimeOffsetAdjustment(accordingTo: DateTime.UtcNow));
    
    public string TodayKyivText() => 
      DateTime.UtcNow
        .AddHours(SummerTimeOffsetAdjustment(accordingTo: DateTime.UtcNow))
        .ToStringDateFormat();

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