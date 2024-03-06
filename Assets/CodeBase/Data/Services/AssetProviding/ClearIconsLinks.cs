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

    public ClearIconsLinks(IHolidaysStorage storage, string date)
    {
      LoadIcons(storage, date);
    }

    private void LoadIcons(IHolidaysStorage storage, string date)
    {
      string jsonText = File.ReadAllText(storage.HolidayConfigFor(date));

      var info = jsonText.ToDeserialize<RawHolidayInfo>();

      MainIcon += info.MainImage;

      if (info.DayIcons is { Count: > 0 })
        foreach (string icon in info.DayIcons.Values)
          DayIcons.Add(icon);
    }
  }
}