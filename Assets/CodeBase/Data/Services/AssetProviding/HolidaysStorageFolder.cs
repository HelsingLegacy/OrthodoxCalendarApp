using System.IO;
using CodeBase.Extensions;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Data.Services.AssetProviding
{
  public class HolidaysStorageFolder : IHolidaysStorage
  {
    private const string FolderJsonData = "JsonData";
    private const string FolderIcons = "Icons";
    
    private string AppropriateDataPath =>
      Application.isMobilePlatform ? MobileDataPath() : EditorDataPath();

    public string HolidayConfigFor(string date)
    {
      CreateFolder(FolderJsonData);
      return HolidaySaved(to: AppropriateDataPath, at: FolderJsonData, withFileName: date.Json());
    }

    public string HolidayIconFor(string date)
    {
      CreateFolder(FolderIcons);
      return IconSaved(to: AppropriateDataPath, at: FolderIcons, date.WithoutYear());
    }

    public void CreateFolder(string name)
    {
      string folderJsonData = Path.Combine(AppropriateDataPath, name);
      
      if (!Directory.Exists(folderJsonData)) 
        Directory.CreateDirectory(folderJsonData);
    }

    private string HolidaySaved(string to, string at, string withFileName) =>
      Path.Combine(to, at, withFileName);
    
    private string IconSaved(string to, string at, string withFileName) =>
      Path.Combine(to, at, withFileName);

    private static string MobileDataPath() => Application.persistentDataPath;

    private static string EditorDataPath() => Application.dataPath;
  }
}