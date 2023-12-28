﻿using System.IO;
using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.Data.Services
{
  public class HolidayDataPath : IHolidayDataPath
  {
    private const string FolderJsonData = "JsonData";
    
    private string AppropriateDataPath =>
      Application.isMobilePlatform ? MobileDataPath() : EditorDataPath();

    public string ReadingsFor(string date)
    {
      CreateFolderJsonData();

      return ReadingsSaved(to: AppropriateDataPath, at: FolderJsonData, withFileName: date.Reading().Json());
    }

    private void CreateFolderJsonData()
    {
      string folderJsonData = Path.Combine(AppropriateDataPath, FolderJsonData);
      
      if (!Directory.Exists(folderJsonData)) 
        Directory.CreateDirectory(folderJsonData);
    }

    private string ReadingsSaved(string to, string at, string withFileName) =>
      Path.Combine(to, at, withFileName);

    private static string MobileDataPath() => Application.persistentDataPath;

    private static string EditorDataPath() => Application.dataPath;
  }
}