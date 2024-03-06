using System.IO;
using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.Data.Services.AssetProviding
{
  public class HolidaysStorageFolder : IHolidaysStorage
  {
    private const string FolderJsonData = "JsonData";
    private const string FolderIcons = "Icons";
    
    private string _appropriateDataPath;

    public void BindDataPath() => 
      _appropriateDataPath = Application.isMobilePlatform ? MobileDataPath() : EditorDataPath();

    public string HolidayConfigFor(string date)
    {
      CreateFolder(FolderJsonData);
      return HolidaySaved(to: _appropriateDataPath, at: FolderJsonData, withFileName: date.Json());
    }

    public string HolidayIconFor(string date)
    {
      CreateFolder(FolderIcons);
      return IconSaved(to: _appropriateDataPath, at: FolderIcons, date);
    }

    private void CreateFolder(string name)
    {
      if (!Directory.Exists(Path.Combine(_appropriateDataPath, name))) 
        Directory.CreateDirectory(Path.Combine(_appropriateDataPath, name));
    }

    private string HolidaySaved(string to, string at, string withFileName) =>
      Path.Combine(to, at, withFileName);
    
    private string IconSaved(string to, string at, string withFileName) =>
      Path.Combine(to, at, withFileName);

    private static string MobileDataPath() => Application.persistentDataPath;

    private static string EditorDataPath() => Application.dataPath;
  }
}