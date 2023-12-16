using System.Collections;
using System.IO;
using CodeBase.Extensions;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Data.Services
{
  public class JsonSaver
  {
    private const string TodayHolidayLink = "https://orthodox-calendar.com.ua/wp-json/calendar/v1/today/?reading=true";

    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IHolidayDataPath _holidayDataPath;

    public JsonSaver(ICoroutineRunner coroutineRunner, IHolidayDataPath holidayDataPath)
    {
      _coroutineRunner = coroutineRunner;
      _holidayDataPath = holidayDataPath;
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
          jsonText = jsonText.RemoveUnnecessaryEscape();
          
          File.WriteAllTextAsync(_holidayDataPath.TodayReadingsLocation(), 
            jsonText);

          Debug.Log("JSON saved to " + _holidayDataPath.TodayReadingsLocation());
        }
        else
          Debug.LogError("Error for loading JSON from server: " + www.error);
      }
    }

    private bool RequestedFileExist() => 
      File.Exists(_holidayDataPath.TodayReadingsLocation());

  }
}