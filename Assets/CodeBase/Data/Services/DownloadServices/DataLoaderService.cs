using System.IO;
using CodeBase.Data.Services.AssetProviding;
using CodeBase.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace CodeBase.Data.Services.DownloadServices
{
  public class DataLoaderService : IDataLoaderService
  {
    private readonly IHolidaysStorage _holidaysStorage;
    private readonly ILinkProvider _linkProvider;

    public DataLoaderService(IHolidaysStorage holidaysStorage, ILinkProvider linkProvider)
    {
      _holidaysStorage = holidaysStorage;
      _linkProvider = linkProvider;
    }

    public async UniTask<float> LoadJson(string date)
    {
      string link = _linkProvider.HolidayLink();
      string parameters = _linkProvider.ReadingParameter();
      
      string webLink = link + date + parameters;
      
      using (UnityWebRequest www = UnityWebRequest.Get(webLink))
      {
        await www.SendWebRequest();

        if (IsSuccess(www))
        {
          return await OnSuccess(date, www);
        }
        
        return 0f;
      }

      async UniTask<float> OnSuccess(string s, UnityWebRequest www)
      {
        string jsonText = www.downloadHandler.text;
        jsonText = jsonText
          .RemoveUnnecessaryEscape()
          .RemoveHtmlTags();
          
        await UniTask.RunOnThreadPool(() => 
          File.WriteAllTextAsync(
            _holidaysStorage.HolidayFor(s), 
            jsonText));
        return 1f;
      }

      bool IsSuccess(UnityWebRequest www) => 
        www.result == UnityWebRequest.Result.Success;
    }
  }
}