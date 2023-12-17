using System;

namespace CodeBase.Data.Services
{
  public class KyivDate : IKyivDate
  {
    public bool TodayIs(DateTime currentDate)
    {
      int today = DateTime.Compare(currentDate, TodayKyivMidnightPassed().Date);
      int tomorrow = DateTime.Compare(currentDate, TodayKyivMidnightPassed().AddDays(1).Date);

      if (today >= 0 && tomorrow < 0)
        return true;
      return false;
    }

    private DateTime TodayKyivMidnightPassed() => 
      DateTime.UtcNow.AddHours(SummerTimeOffsetAdjustment(DateTime.UtcNow));

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