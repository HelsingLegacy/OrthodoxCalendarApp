using System;
using System.Collections.Generic;
using System.IO;
using CodeBase.Data.Services.AssetProviding;
using CodeBase.Extensions;
using CodeBase.Infrastructure.Services.TimeDate;
using Cysharp.Threading.Tasks;

namespace CodeBase.Data.Services.DownloadServices
{
  public class DownloadingService : IDownloadingService
  {
    private readonly IDataLoaderService _dataLoader;
    private readonly DownloadReporterService _downloadReporter;
    private readonly IKyivDate _kyivDate;
    private readonly IHolidaysStorage _holidaysStorage;

    public DownloadingService(IDataLoaderService dataLoader, DownloadReporterService downloadReporter, IKyivDate kyivDate, IHolidaysStorage holidaysStorage)
    {
      _dataLoader = dataLoader;
      _downloadReporter = downloadReporter;
      _kyivDate = kyivDate;
      _holidaysStorage = holidaysStorage;
    }

    private async UniTask DownloadContentWithPreciseProgress(List<string> dates)
    {
      

      _downloadReporter.Reset();
    }
    
    public async void LoadHolidays()
    {
      DateTime startDate = _kyivDate.StartDate();
      DateTime endDate = _kyivDate.EndDate();

      for (DateTime currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
      {
        string date = currentDate.ToStringDateFormat();

        if (!RequestedFileExist(date))
        {
          await _dataLoader.LoadJson(date);
        }
      }
    }

    private bool RequestedFileExist(string date) =>
      File.Exists(_holidaysStorage.HolidayFor(date));
  }
}