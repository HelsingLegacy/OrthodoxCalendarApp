using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.Services.TimeDate
{
  public interface IKyivDate : IService
  {
    DateTime StartDate();
    DateTime EndDate();
    List<string> DaysFor(Month month, string year);
  }
}