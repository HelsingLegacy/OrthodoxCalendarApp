using System;

namespace CodeBase.Infrastructure.Services.TimeDate
{
  public class KyivDate : IKyivDate
  {
    private DateTime CurrentMonthFirstDay => new(TodayKyiv().Year, TodayKyiv().Month, 1);
    private DateTime CurrentMonthLastDay => CurrentMonthFirstDay.AddMonths(1).AddDays(-1);
    
    public DateTime StartDate() => 
      CurrentMonthFirstDay;

    public DateTime EndDate() => 
      CurrentMonthLastDay;

    private DateTime TodayKyiv() => 
      DateTime.UtcNow.AddHours(SummerTimeOffsetAdjustment(accordingTo: DateTime.UtcNow));

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