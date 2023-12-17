using System;
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
    private const string TodayParameter = "today";
    private const string ReadingParameter = "/?reading=true";

    private readonly ICoroutineRunner _coroutineRunner;
    private readonly IHolidayDataPath _holidayDataPath;
    private readonly IKyivDate _kyivDate;

    public JsonSaver(ICoroutineRunner coroutineRunner, IHolidayDataPath holidayDataPath, IKyivDate kyivDate)
    {
      _coroutineRunner = coroutineRunner;
      _holidayDataPath = holidayDataPath;
      _kyivDate = kyivDate;
    }

    public void LoadJsonFor(DateTime dateParameter) => 
      _coroutineRunner.StartCoroutine(LoadJson(dateParameter));

    private IEnumerator LoadJson(DateTime date)
    {
      string webLink = HolidayLink + ParameterFor(date) + ReadingParameter;
      
      if (RequestedFileExist(forThis: date.ToDateFormat()))
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
          
          File.WriteAllTextAsync(_holidayDataPath.ReadingsFor(date.ToDateFormat()), 
            jsonText);
        }
        else
          Debug.LogError("Error for loading JSON from server: " + www.error);
      }
    }

    private string ParameterFor(DateTime date) =>
      !_kyivDate.TodayIs(date) 
        ? date.ToDateFormat() 
        : TodayParameter;

    private bool RequestedFileExist(string forThis) => 
      File.Exists(_holidayDataPath.ReadingsFor(forThis));
  }
}