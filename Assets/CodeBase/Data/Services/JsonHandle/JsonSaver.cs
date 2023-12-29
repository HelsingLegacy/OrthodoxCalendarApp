using System.IO;
using CodeBase.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Data.Services.JsonHandle
{
  public class JsonSaver : IJsonSaver
  {
    private readonly IHolidaysStorage _holidaysStorage;
    private readonly ILinkProvider _linkProvider;

    public JsonSaver(IHolidaysStorage holidaysStorage, ILinkProvider linkProvider)
    {
      _holidaysStorage = holidaysStorage;
      _linkProvider = linkProvider;
    }

    public async void LoadJsonFor(string dateParameter) => 
      await LoadJson(dateParameter);

    private async UniTask<float> LoadJson(string date)
    {
      string link = _linkProvider.HolidayLink();
      string parameters = _linkProvider.ReadingParameter();
      
      string webLink = link + date + parameters;
      
      using (UnityWebRequest www = UnityWebRequest.Get(webLink))
      {
        await www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
          string jsonText = www.downloadHandler.text;
          jsonText = jsonText
            .RemoveUnnecessaryEscape()
            .RemoveHtmlTags();
          
          await UniTask.RunOnThreadPool(() => 
              File.WriteAllTextAsync(
              _holidaysStorage.HolidayFor(date), 
              jsonText));
          return 1f;
        }
        else
          Debug.LogError("Error for loading JSON from server: " + www.error);

        return 0f;
      }
    }
  }
}