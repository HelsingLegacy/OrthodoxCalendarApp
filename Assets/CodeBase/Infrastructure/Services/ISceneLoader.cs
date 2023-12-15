using System;
using CodeBase.Data.Services;

namespace CodeBase.Infrastructure.Services
{
  public interface ISceneLoader : IService
  {
    void LoadScene(string name, Action onLoaded = null);
  }
}