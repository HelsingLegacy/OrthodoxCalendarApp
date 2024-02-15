using System;

namespace CodeBase.Data.Services.DownloadServices
{
  public interface IDownloadingService
  {
    void LoadHoliday(string date, Action onLoaded = null);
    void LoadHolidays( Action onLoaded);
  }
}