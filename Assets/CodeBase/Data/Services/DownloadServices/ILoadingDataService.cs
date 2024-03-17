using Cysharp.Threading.Tasks;

namespace CodeBase.Data.Services.DownloadServices
{
  public interface ILoadingDataService
  {
    UniTask LoadRawHoliday(string date);
    UniTask LoadIcons(string date);
  }
}