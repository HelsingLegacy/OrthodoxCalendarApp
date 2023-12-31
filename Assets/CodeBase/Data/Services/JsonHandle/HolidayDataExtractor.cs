using System.IO;
using CodeBase.Extensions;
using CodeBase.Infrastructure.Services.TimeDate;

namespace CodeBase.Data.Services.JsonHandle
{
  public class HolidayDataExtractor : IHolidayDataExtractor
  {
    private readonly IHolidaysStorage _storage;
    private readonly IToday _today;

    public HolidayDataExtractor(IHolidaysStorage storage, IToday today)
    {
      _storage = storage;
      _today = today;
    }
    
    public RawHolidayInfo ExtractedData()
    {
      string jsonFilePath = _storage.HolidayFor(_today.TodayKyiv().ToStringDateFormat());

      string jsonText = File.ReadAllText(jsonFilePath);

      RawHolidayInfo info = jsonText.ToDeserialize<RawHolidayInfo>();
      
      return info;
    }
  }
}