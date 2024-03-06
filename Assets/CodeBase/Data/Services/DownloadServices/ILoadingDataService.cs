using Cysharp.Threading.Tasks;

namespace CodeBase.Data.Services.DownloadServices
{
  public interface ILoadingDataService
  {
    UniTask LoadRawHoliday(string dates);
    UniTask LoadIcons(string date);
  }
}