using System;

namespace CodeBase.Data.Services
{
  public class TimeCorrection : ITimeCorrection
  {
    private const string DateFormat = "dd-MM-yyyy";

    public string KyivCurrentDate()
    {
      DateTime utcNow = DateTime.UtcNow;

      DateTime currentLocalTime = utcNow.AddHours(SummerTimeOffsetAdjustment(accordingTo: utcNow));

      return currentLocalTime.ToString(DateFormat);
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