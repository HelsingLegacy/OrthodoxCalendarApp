using System;

namespace CodeBase.Data.Services.DownloadServices
{
  public interface IDownloadingService
  {
    void LoadHoliday( Action onLoaded);
    void LoadHolidays( Action onLoaded);
  }
}