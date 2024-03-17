using System.IO;
using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.Data.Services.AssetProviding
{
  public class HolidaysDataStorage : IHolidaysDataStorage, IStorageConfiguration
  {
    private const string FolderJsonData = "JsonData";
    private const string FolderIcons = "Icons";
    
    private string _appropriateDataPath;

    public void BindDataPath() => 
      _appropriateDataPath = Application.isMobilePlatform ? MobileDataPath() : EditorDataPath();

    public string HolidayConfigFor(string date) => 
      HolidayLocation(to: _appropriateDataPath, at: FolderJsonData, withFileName: date.Json());

    public string HolidayIconFor(string date) => 
      IconLocation(to: _appropriateDataPath, at: FolderIcons, date);

    public void CreateDataFolders()
    {
      if (!Directory.Exists(Path.Combine(_appropriateDataPath, FolderJsonData))) 
        Directory.CreateDirectory(Path.Combine(_appropriateDataPath, FolderJsonData));
      
      if (!Directory.Exists(Path.Combine(_appropriateDataPath, FolderIcons))) 
        Directory.CreateDirectory(Path.Combine(_appropriateDataPath, FolderIcons));
    }

    private string HolidayLocation(string to, string at, string withFileName) =>
      Path.Combine(to, at, withFileName);
    
    private string IconLocation(string to, string at, string withFileName) =>
      Path.Combine(to, at, withFileName);

    private static string MobileDataPath() => Application.persistentDataPath;

    private static string EditorDataPath() => Application.dataPath;
  }
}