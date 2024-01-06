using Cysharp.Threading.Tasks;

namespace CodeBase.Data.Services.DownloadServices
{
  public interface IDataLoaderService
  {
    UniTask<float> LoadJson(string date);
  }
}