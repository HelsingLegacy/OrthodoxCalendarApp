using CodeBase.Infrastructure.Services;

namespace CodeBase.Data.Services
{
  public interface IKyivDate : IService
  {
    string Today { get;}
  }
}