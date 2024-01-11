using System.IO;
using CodeBase.Data.Services.AssetProviding;
using CodeBase.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
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

    public async UniTask<int> LoadRawHoliday(string dates)
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
            _holidaysStorage.HolidayConfigFor(forDate),
            jsonText));

        return 1;
      }

      bool IsSuccess(UnityWebRequest www) =>
        www.result == UnityWebRequest.Result.Success;
    }

    public async UniTask<float> LoadIcons(string date)
    {
      ClearIconsLinks links = new ClearIconsLinks(_holidaysStorage, date);

      float value = 0f;
      int completedValue = links.DayIcons.Count + 1;

      string pathAndName = Path.Combine(_holidaysStorage.HolidayIconFor(date));

      string mainIcon = links.MainIcon;

      await LoadIconFor(mainIcon, with: pathAndName);
      
      if (links.DayIcons is { Count: > 0 }) 
        for (int i = 1; i <= links.DayIcons.Count; i++) 
          await LoadIconFor(links.DayIcons[i - 1], pathAndName + $" ({i})");

      return value;
      
      async UniTask LoadIconFor(string iconLink, string with)
      {
        using (UnityWebRequest webLink = UnityWebRequest.Get(iconLink))
        {
          await webLink.SendWebRequest();

          if (IsSuccess(webLink))
          {
            byte[] texture = webLink.downloadHandler.data;

            await File.WriteAllBytesAsync(with, texture);
            
            if(completedValue > 0)
              value += completedValue / completedValue;
          }
          else
          {
            Debug.LogError("Can't load main icon. Added to List for next Downloading (still not done)");
          }


          bool IsSuccess(UnityWebRequest www) =>
            www.result == UnityWebRequest.Result.Success;
        }
      }
    }
  }
}