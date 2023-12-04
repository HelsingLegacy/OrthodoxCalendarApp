using System;

namespace CodeBase.Infrastructure.Services
{
  public interface ISceneLoader
  {
    void LoadScene(string name, Action onLoaded = null);
  }
}