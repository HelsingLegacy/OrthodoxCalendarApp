using CodeBase.Data.Services.AssetProviding;
using CodeBase.Infrastructure.Services.TimeDate;

namespace CodeBase.Infrastructure.Services
{
  public class ConfigProvider : IConfigProvider
  {
    private readonly IHolidaysStorage _storage;
    private readonly IToday _today;

    public ConfigProvider(IHolidaysStorage storage, IToday today)
    {
      _storage = storage;
      _today = today;
    }

    public ConfigAssembly GetConfigFor(string date) => 
      new(_storage, date);

    public ConfigAssembly GetConfigForToday() => 
      GetConfigFor(_today.TodayKyivText());
  }
}