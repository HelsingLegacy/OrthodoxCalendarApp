using System;
using CodeBase.Infrastructure.Services.TimeDate;
using Cysharp.Threading.Tasks;

namespace CodeBase.Data.Services.DownloadServices
{
  public interface IDownloadingService
  {
    UniTask DownloadHoliday(string date, Action onLoaded = null);
    UniTask DownloadHolidays(Month month, string year);
  }
}