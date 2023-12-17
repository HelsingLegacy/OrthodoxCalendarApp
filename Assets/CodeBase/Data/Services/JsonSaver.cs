using System.Collections;
using System.IO;
using CodeBase.Extensions;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Data.Services
{
  public class JsonSaver : IJsonSaver
  {
    private const string HolidayLink = "https://orthodox-calendar.com.ua/wp-json/calendar/v1/";
    public const string TodayParameter = "today";
    private const string ReadingParameter = "/?reading=true";

    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IHolidayDataPath _holidayDataPath;

    public JsonSaver(ICoroutineRunner coroutineRunner, IHolidayDataPath holidayDataPath)
    {
      _coroutineRunner = coroutineRunner;
      _holidayDataPath = holidayDataPath;
    }

    public void LoadJsonFor(string dateParameter = TodayParameter) => 
      _coroutineRunner.StartCoroutine(LoadJson(dateParameter));

    private IEnumerator LoadJson(string date)
    {
      string webLink = HolidayLink + date + ReadingParameter;
      
      if (RequestedFileExist(date))
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
          jsonText = jsonText
            .RemoveUnnecessaryEscape()
            .RemoveHtmlTags();
          
          File.WriteAllTextAsync(_holidayDataPath.ReadingsFor(date), 
            jsonText);

          Debug.Log("JSON saved to " + _holidayDataPath.ReadingsFor(date));
        }
        else
          Debug.LogError("Error for loading JSON from server: " + www.error);
      }
    }

    private bool RequestedFileExist(string date) => 
      File.Exists(_holidayDataPath.ReadingsFor(date));
  }
}