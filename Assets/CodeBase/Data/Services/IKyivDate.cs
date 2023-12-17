using System;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Data.Services
{
  public interface IKyivDate : IService
  {
    bool TodayIs(DateTime currentDate);
  }
}