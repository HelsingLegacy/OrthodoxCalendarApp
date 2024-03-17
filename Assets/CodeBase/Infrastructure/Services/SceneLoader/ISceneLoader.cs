using System;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
  public interface ISceneLoader
  {
    void LoadScene(string name, Action onLoaded = null);
  }
}