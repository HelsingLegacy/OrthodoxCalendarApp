using System.Collections.Generic;
using System.IO;
using CodeBase.Data.DeserializationClasses;
using CodeBase.Extensions;

namespace CodeBase.Data.Services.AssetProviding
{
  public class ClearIconsLinks
  {
    public string MainIcon = "";
    public List<string> DayIcons = new();

    public ClearIconsLinks(IHolidaysDataStorage dataStorage, string date)
    {
      LoadIcons(dataStorage, date);
    }

    private void LoadIcons(IHolidaysDataStorage dataStorage, string date)
    {
      string jsonText = File.ReadAllText(dataStorage.HolidayConfigFor(date));

      var info = jsonText.ToDeserialize<RawHolidayInfo>();

      MainIcon += info.MainImage;

      if (info.DayIcons is { Count: > 0 })
        foreach (string icon in info.DayIcons.Values)
          DayIcons.Add(icon);
    }
  }
}