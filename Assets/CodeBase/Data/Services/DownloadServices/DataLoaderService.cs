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

    public async UniTask<int> LoadJson(string dates)
    {
      string link = _linkProvider.HolidayLink();
      string parameters = _linkProvider.ReadingParameter();
      
      string url = link + dates + parameters;
      
      using (UnityWebRequest webLink = UnityWebRequest.Get(url))
      {
        await webLink.SendWebRequest();

        if (IsSuccess(webLink))
        {
          return await CreateDataConfig(forDate: dates, from: webLink);
        }
        
        return 0;
      }

      async UniTask<int> CreateDataConfig(string forDate, UnityWebRequest from)
      {
        string jsonText = from.downloadHandler.text;
        jsonText = jsonText
          .RemoveUnnecessaryEscape()
          .RemoveHtmlTags();
          
        await UniTask.RunOnThreadPool(async () => 
          await File.WriteAllTextAsync(
            _holidaysStorage.HolidayFor(forDate), 
            jsonText));
        return 1;
      }

      bool IsSuccess(UnityWebRequest www) => 
        www.result == UnityWebRequest.Result.Success;
    }
  }
}