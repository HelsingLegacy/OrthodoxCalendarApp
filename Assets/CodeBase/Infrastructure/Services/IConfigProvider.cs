using CodeBase.Data.Services.AssetProviding;

namespace CodeBase.Infrastructure.Services
{
  public interface IConfigProvider
  {
    ConfigAssembly GetConfigFor(string date);
    ConfigAssembly GetConfigForToday();
  }
}