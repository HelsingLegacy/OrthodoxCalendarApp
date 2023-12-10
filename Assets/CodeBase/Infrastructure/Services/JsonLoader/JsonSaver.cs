using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Infrastructure.Services.JsonLoader
{
  public class JsonSaver
  {
    private const string TodayHolidayLink = "https://orthodox-calendar.com.ua/wp-json/calendar/v1/today/?reading=true";

    private string _filePath =
      Path.Combine(Application.dataPath, "JsonData", DateTimeOffset.Now.ToString("dd-MM-yyyy") + ".json");

    private readonly ICoroutineRunner _coroutineRunner;

    public JsonSaver(ICoroutineRunner coroutineRunner)
    {
      _coroutineRunner = coroutineRunner;
    }

    public void LoadJsonFromServer()
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

      string util = JsonUtility.ToJson(LoadJsonFrom(TodayHolidayLink));

      using (UnityWebRequest www = UnityWebRequest.Get(webLink))
      {
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
          string jsonText = www.downloadHandler.text;


          Task writeText = File.WriteAllTextAsync(_filePath, jsonText);

          while (!writeText.IsCompleted)
            yield return null;

          Debug.Log("JSON saved to " + _filePath);
        }
        else
        {
          Debug.LogError("Error for loading JSON from server: " + www.error);
        }
      }
    }

    private bool RequestedFileExist()
    {
      return File.Exists(_filePath);
    }
  }
}