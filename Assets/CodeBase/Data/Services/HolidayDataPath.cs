using System.IO;
using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.Data.Services
{
  public class HolidayDataPath : IHolidayDataPath
  {
    private readonly IKyivToday _kyivToday;
    private const string FolderJsonData = "JsonData";

    private string AppropriateDataPath =>
      Application.isMobilePlatform ? MobileDataPath() : EditorDataPath();

    public HolidayDataPath(IKyivToday kyivToday)
    {
      _kyivToday = kyivToday;
    }

    public string TodayReadingsLocation() => 
      ReadingsSaved(to: AppropriateDataPath, at: FolderJsonData, withName: _kyivToday.Date.Reading().Json());

    private string ReadingsSaved(string to, string at, string withName) =>
      Path.Combine(to, at, withName);

    private static string MobileDataPath() => Application.persistentDataPath;

    private static string EditorDataPath() => Application.dataPath;
  }
}