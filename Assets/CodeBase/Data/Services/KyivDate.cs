using System;

namespace CodeBase.Data.Services
{
  public class KyivDate : IKyivDate
  {
    public bool TodayIs(DateTime currentDate)
    {
      int today = DateTime.Compare(currentDate, TodayKyiv().Date);
      int tomorrow = DateTime.Compare(currentDate, TodayKyiv().AddDays(1).Date);
      
      return NowIs(today) && NowIsNot(tomorrow);
    }

    public DateTime StartDate() => 
      new(TodayKyiv().Year, 12, 12);

    public DateTime EndDate() => 
      new(TodayKyiv().Year, 12, 31);

    private static bool NowIs(int today) => today >= 0;

    private static bool NowIsNot(int tomorrow) => tomorrow < 0;

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