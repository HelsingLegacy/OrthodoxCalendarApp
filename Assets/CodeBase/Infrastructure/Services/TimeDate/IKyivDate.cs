using System.Collections.Generic;

namespace CodeBase.Infrastructure.Services.TimeDate
{
  public interface IKyivDate
  {
    List<string> DaysFor(Month month, string year);
  }
}