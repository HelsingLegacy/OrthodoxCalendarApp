using System;

namespace CodeBase.Infrastructure.Services.TimeDate
{
  public interface IToday
  {
    DateTime TodayKyivDate();

    string TodayKyivText();
  }
}