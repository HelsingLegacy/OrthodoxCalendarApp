using System.IO;
using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.Data.Services
{
  public class HolidayDataPath : IHolidayDataPath
  {
    private const string FolderJsonData = "JsonData";

    private string AppropriateDataPath =>
      Application.isMobilePlatform ? MobileDataPath() : EditorDataPath();

    public string ReadingsFor(string date) => 
      ReadingsSaved(to: AppropriateDataPath, at: FolderJsonData, withFileName: date.Reading().Json());

    private string ReadingsSaved(string to, string at, string withFileName) =>
      Path.Combine(to, at, withFileName);

    private static string MobileDataPath() => Application.persistentDataPath;

    private static string EditorDataPath() => Application.dataPath;
  }
}