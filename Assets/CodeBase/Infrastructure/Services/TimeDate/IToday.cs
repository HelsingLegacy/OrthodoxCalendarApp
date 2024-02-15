using System;

namespace CodeBase.Infrastructure.Services.TimeDate
{
  public interface IToday : IService
  {
    DateTime TodayKyivDate();

    string TodayKyivText();
  }
}