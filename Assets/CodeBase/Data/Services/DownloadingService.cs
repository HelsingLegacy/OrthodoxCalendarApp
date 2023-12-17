using System;

namespace CodeBase.Data.Services
{
  public class DownloadingService : IDownloadingService
  {
    private IJsonSaver _jsonSaver;

    public DownloadingService(IJsonSaver jsonSaver)
    {
      _jsonSaver = jsonSaver;
    }

    public void LoadYearOrToday()
    {
      DateTime startDate = new DateTime(2023, 12, 16);

      DateTime endDate = new DateTime(2023, 12, 31);
      
      for(DateTime currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
      {
        _jsonSaver.LoadJsonFor(currentDate);
      }    
    }
  }
}