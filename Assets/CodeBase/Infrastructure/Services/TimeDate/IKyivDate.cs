using System.Collections.Generic;

namespace CodeBase.Infrastructure.Services.TimeDate
{
  public interface IKyivDate : IService
  {
    List<string> DaysFor(Month month, string year);
  }
}