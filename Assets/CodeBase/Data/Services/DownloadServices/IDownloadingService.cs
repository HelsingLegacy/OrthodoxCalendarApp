using System;
using Cysharp.Threading.Tasks;

namespace CodeBase.Data.Services.DownloadServices
{
  public interface IDownloadingService
  {
    UniTask LoadHoliday(string date, Action onLoaded = null);
    void LoadHolidays( Action onLoaded);
  }
}