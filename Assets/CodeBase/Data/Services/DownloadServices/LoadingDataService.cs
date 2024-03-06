using System.IO;
using CodeBase.Data.Services.AssetProviding;
using CodeBase.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Data.Services.DownloadServices
{
  public class LoadingDataService : ILoadingDataService
  {
    private readonly IHolidaysStorage _holidaysStorage;
    private readonly ILinkProvider _linkProvider;

    public LoadingDataService(IHolidaysStorage holidaysStorage, ILinkProvider linkProvider)
    {
      _holidaysStorage = holidaysStorage;
      _linkProvider = linkProvider;
    }

    public async UniTask LoadRawHoliday(string dates)
    {
      string link = _linkProvider.HolidayLink();
      string parameters = _linkProvider.ReadingParameter();

      string url = link + dates + parameters;

      using (UnityWebRequest webLink = UnityWebRequest.Get(url))
      {
        await webLink.SendWebRequest();

        if (IsSuccess(webLink)) 
          await CreateDataConfig(forDate: dates, from: webLink);
      }

      async UniTask CreateDataConfig(string forDate, UnityWebRequest from)
      {
        string jsonText = from.downloadHandler.text;
        jsonText = jsonText
          .RemoveUnnecessaryEscape()
          .RemoveHtmlTags();

        await UniTask.RunOnThreadPool(async () =>
          await File.WriteAllTextAsync(
            _holidaysStorage.HolidayConfigFor(forDate),
            jsonText));
      }

      bool IsSuccess(UnityWebRequest www) =>
        www.result == UnityWebRequest.Result.Success;
    }

    public async UniTask LoadIcons(string date)
    {
      ClearIconsLinks links = new ClearIconsLinks(_holidaysStorage, date);

      string pathAndName = Path.Combine(_holidaysStorage.HolidayIconFor(date));

      string mainIcon = links.MainIcon;

      await LoadIconFor(mainIcon, with: pathAndName);
      
      if (links.DayIcons is { Count: > 0 })
      {
        for (int i = 0; i < links.DayIcons.Count; i++)
          await LoadIconFor(links.DayIcons[i], pathAndName.WithIndex(i + 1));
      }
      
      async UniTask LoadIconFor(string iconLink, string with)
      {
        using (UnityWebRequest webLink = UnityWebRequest.Get(iconLink))
        {
          await webLink.SendWebRequest();

          if (IsSuccess(webLink))
          {
            byte[] texture = webLink.downloadHandler.data;

            await File.WriteAllBytesAsync(with, texture);
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