using System;

namespace CodeBase.Infrastructure.Services.TimeDate
{
  public interface IKyivDate : IService
  {
    DateTime StartDate();
    DateTime EndDate();
  }
}