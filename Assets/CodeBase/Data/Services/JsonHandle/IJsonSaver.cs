using Cysharp.Threading.Tasks;

namespace CodeBase.Data.Services.JsonHandle
{
  public interface IJsonSaver
  {
    UniTask<float> LoadJson(string date);
  }
}