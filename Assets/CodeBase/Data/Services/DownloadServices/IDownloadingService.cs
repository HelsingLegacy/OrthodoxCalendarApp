using System;

namespace CodeBase.Data.Services.DownloadServices
{
  public interface IDownloadingService
  {
    void LoadHoliday(string date, Action onLoaded);
    void LoadHolidays( Action onLoaded);
  }
}