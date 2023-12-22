using System;

namespace CodeBase.Data.Services
{
  public class DownloadingService : IDownloadingService
  {
    private readonly IJsonSaver _jsonSaver;
    private readonly IKyivDate _kyivDate;

    public DownloadingService(IJsonSaver jsonSaver, IKyivDate kyivDate)
    {
      _jsonSaver = jsonSaver;
      _kyivDate = kyivDate;
    }

    public void LoadCurrentYear()
    {
      for (DateTime currentDate = _kyivDate.StartDate();
           currentDate <= _kyivDate.EndDate();
           currentDate = currentDate.AddDays(1))
        _jsonSaver.LoadJsonFor(currentDate);
    }
  }
}