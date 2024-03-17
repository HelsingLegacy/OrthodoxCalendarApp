using CodeBase.Data.Services.AssetProviding;

namespace CodeBase.Data.Services
{
  public interface IConfigProvider
  {
    ConfigAssembly GetConfigFor(string date);
    ConfigAssembly GetConfigForToday();
  }
}