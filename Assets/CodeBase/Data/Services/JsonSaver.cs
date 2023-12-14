using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Data.Services
{
  public class JsonSaver
  {
    private const string TodayHolidayLink = "https://orthodox-calendar.com.ua/wp-json/calendar/v1/today/?reading=true";
    private const string FolderJsonData = "JsonData";
    private const string ReadingPrefix = "-reading";
    private const string JsonExtension = ".json";
    private const string DateFormat = "dd-MM-yyyy";

    private readonly ICoroutineRunner _coroutineRunner;

    private string DataPath =>
      Application.isMobilePlatform ? MobileDataPath() : EditorDataPath();

    public JsonSaver(ICoroutineRunner coroutineRunner)
    {
      _coroutineRunner = coroutineRunner;
    }

    public void LoadJsonForToday()
    {
      _coroutineRunner.StartCoroutine(LoadJsonFrom(TodayHolidayLink));
    }

    private IEnumerator LoadJsonFrom(string webLink)
    {
      if (RequestedFileExist())
      {
        Debug.Log("File Exist");
        yield break;
      }

      using (UnityWebRequest www = UnityWebRequest.Get(webLink))
      {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
          string jsonText = www.downloadHandler.text;


          Task writeText = File.WriteAllTextAsync(ReadingsSaved(to: DataPath, at: FolderJsonData), jsonText);

          while (!writeText.IsCompleted)
            yield return null;

          Debug.Log("JSON saved to " + ReadingsSaved(MobileDataPath(), FolderJsonData));
        }
        else
        {
          Debug.LogError("Error for loading JSON from server: " + www.error);
        }
      }
    }

    // Shift this method to separated service DataCorrectionService
    private string KyivCurrentDate()
    {
      DateTime utcTime = DateTime.UtcNow;

      int timeOffsetHours = 2;

      if (utcTime.Date >= new DateTime(2024, 3, 31))
        timeOffsetHours = 3;

      DateTime currentLocalTime = utcTime.AddHours(timeOffsetHours);

      return currentLocalTime.ToString(DateFormat);
    }

    private bool RequestedFileExist()
    {
      return File.Exists(ReadingsSaved(DataPath, FolderJsonData));
    }

    private string ReadingsSaved(string to, string at) =>
      Path.Combine(to, at, KyivDateReadingJson());

    private string KyivDateReadingJson()
    {
      return KyivCurrentDate() + ReadingPrefix + JsonExtension;
    }

    private static string MobileDataPath() => Application.persistentDataPath;

    private static string EditorDataPath() => Application.dataPath;
  }
}