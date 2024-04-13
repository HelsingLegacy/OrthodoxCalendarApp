using CodeBase.Infrastructure.Services.Assets;
using CodeBase.UI.ContentHandlers.NonInteracting;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.Factory
{
  class ProgressFactory : IProgressFactory
  {
    private readonly IAssetProvider _provider;
    private readonly DiContainer _container;

    public ProgressFactory(IAssetProvider provider, DiContainer container)
    {
      _provider = provider;
      _container = container;
    }

    public ProgressBar CreateProgress(int days)
    {
      ProgressBar bar = Instantiate(_provider.ProgressBar()).GetComponent<ProgressBar>();

      bar.SetBarValues(size: days);
      
      return bar;
    }

    private GameObject Instantiate(GameObject prefab) =>
      _container.InstantiatePrefab(prefab);  }
}