using CodeBase.Infrastructure.Services;

namespace CodeBase.Data.Services
{
  public interface ITimeCorrection : IService
  {
    string KyivCurrentDate();
  }
}