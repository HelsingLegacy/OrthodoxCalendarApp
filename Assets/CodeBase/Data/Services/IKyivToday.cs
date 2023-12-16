using CodeBase.Infrastructure.Services;

namespace CodeBase.Data.Services
{
  public interface IKyivToday : IService
  {
    string Date { get;}
  }
}