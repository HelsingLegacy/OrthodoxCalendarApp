using System;

namespace CodeBase.Infrastructure
{
  public interface ISceneLoader
  {
    void LoadScene(string name, Action onLoaded = null);
  }
}