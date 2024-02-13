using System;

namespace CodeBase.Infrastructure.Services.TimeDate
{
  public interface IToday : ITodayDate, ITodayText { }
  
  public interface ITodayDate
  {
    DateTime TodayKyivDate();
  }
  
  public interface ITodayText
  {
    string TodayKyivText();
  }
}