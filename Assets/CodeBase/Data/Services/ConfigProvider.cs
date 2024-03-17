using CodeBase.Data.Services.AssetProviding;
using CodeBase.Infrastructure.Services.TimeDate;

namespace CodeBase.Data.Services
{
  public class ConfigProvider : IConfigProvider
  {
    private readonly IHolidaysDataStorage _dataStorage;
    private readonly IToday _today;

    public ConfigProvider(IHolidaysDataStorage dataStorage, IToday today)
    {
      _dataStorage = dataStorage;
      _today = today;
    }

    public ConfigAssembly GetConfigFor(string date) => 
      new(_dataStorage, date);

    public ConfigAssembly GetConfigForToday() => 
      GetConfigFor(_today.TodayKyivText());
  }
}