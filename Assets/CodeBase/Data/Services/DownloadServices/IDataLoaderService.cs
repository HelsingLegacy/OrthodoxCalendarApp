using Cysharp.Threading.Tasks;

namespace CodeBase.Data.Services.DownloadServices
{
  public interface IDataLoaderService
  {
    UniTask<int> LoadJson(string dates);
  }
}