using Cysharp.Threading.Tasks;

namespace CodeBase.Data.Services.DownloadServices
{
  public interface IDataLoaderService
  {
    UniTask<int> LoadRawHoliday(string dates);
    UniTask<float> LoadIcons(string date);
  }
}