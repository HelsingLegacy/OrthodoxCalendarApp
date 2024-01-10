using System;

namespace CodeBase.Infrastructure.Services.TimeDate
{
  public interface IKyivDate : IService, IToday
  {
    DateTime StartDate();
    DateTime EndDate();
  }
}