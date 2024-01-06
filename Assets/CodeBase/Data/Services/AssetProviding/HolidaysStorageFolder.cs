using System.IO;
using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.Data.Services.AssetProviding
{
  public class HolidaysStorageFolder : IHolidaysStorage, IHolidaysStorageFolderCreator
  {
    private const string FolderJsonData = "JsonData";
    
    private string AppropriateDataPath =>
      Application.isMobilePlatform ? MobileDataPath() : EditorDataPath();

    public string HolidayFor(string date) => 
      HolidaySaved(to: AppropriateDataPath, at: FolderJsonData, withFileName: date.Json());

    public void CreateFolderJsonData()
    {
      string folderJsonData = Path.Combine(AppropriateDataPath, FolderJsonData);
      
      if (!Directory.Exists(folderJsonData)) 
        Directory.CreateDirectory(folderJsonData);
    }

    private string HolidaySaved(string to, string at, string withFileName) =>
      Path.Combine(to, at, withFileName);

    private static string MobileDataPath() => Application.persistentDataPath;

    private static string EditorDataPath() => Application.dataPath;
  }
}