using System;
using System.IO;
using CodeBase.Data.Services.AssetProviding;
using CodeBase.Extensions;
using CodeBase.Infrastructure.Services.ErrorHandling;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace CodeBase.Data.Services.DownloadServices
{
  public class LoadingDataService : ILoadingDataService
  {
    private readonly IHolidaysDataStorage _holidaysDataStorage;
    private readonly ILinkProvider _linkProvider;
    private readonly IErrorSaver _errorSaver;

    public LoadingDataService(IHolidaysDataStorage holidaysDataStorage, ILinkProvider linkProvider,
      IErrorSaver errorSaver)
    {
      _holidaysDataStorage = holidaysDataStorage;
      _linkProvider = linkProvider;
      _errorSaver = errorSaver;
    }

    public async UniTask LoadRawHoliday(string date)
    {
      string link = _linkProvider.HolidayLink();
      string parameters = _linkProvider.ReadingParameter();

      string url = link + date + parameters;

      using (UnityWebRequest webLink = UnityWebRequest.Get(url))
      {
        await TrySendWebRequestFor(webLink);

        if (ConnectionError(webLink) || ProtocolError(webLink) || DataProcessingError(webLink))
        {
          _errorSaver.SetErrorCode(ErrorID.RespondFailure);
        }
        else if (IsSuccess(webLink))
          await CreateDataConfig(forDate: date, from: webLink);
      }
    }

    public async UniTask LoadIcons(string date)
    {
      ClearIconsLinks links = new ClearIconsLinks(_holidaysDataStorage, date);

      string pathAndName = Path.Combine(_holidaysDataStorage.HolidayIconFor(date));

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
          await TrySendWebRequestFor(webLink);

          if (ConnectionError(webLink)
              || ProtocolError(webLink)
              || DataProcessingError(webLink))
          {
            _errorSaver.SetErrorCode(ErrorID.RespondFailure);
          }
          else if (IsSuccess(webLink))
          {
            byte[] texture = webLink.downloadHandler.data;

            await File.WriteAllBytesAsync(with, texture);
          }
        }
      }
    }

    private async UniTask TrySendWebRequestFor(UnityWebRequest webLink)
    {
      try
      {
        await webLink.SendWebRequest();
      }
      catch (Exception)
      {
        _errorSaver.SetErrorCode(ErrorID.ConnectionFailure);
      }
    }

    private async UniTask CreateDataConfig(string forDate, UnityWebRequest from)
    {
      string jsonText = from.downloadHandler.text;
      jsonText = jsonText
        .RemoveUnnecessaryEscape()
        .RemoveHtmlTags();

      await UniTask.RunOnThreadPool(async () =>
        await File.WriteAllTextAsync(
          _holidaysDataStorage.HolidayConfigFor(forDate),
          jsonText));
    }

    private bool IsSuccess(UnityWebRequest www) =>
      www.result == UnityWebRequest.Result.Success;

    private bool ConnectionError(UnityWebRequest webLink) =>
      webLink.result == UnityWebRequest.Result.ConnectionError;

    private bool ProtocolError(UnityWebRequest webLink) =>
      webLink.result == UnityWebRequest.Result.ProtocolError;

    private bool DataProcessingError(UnityWebRequest webLink) =>
      webLink.result == UnityWebRequest.Result.DataProcessingError;
  }
}