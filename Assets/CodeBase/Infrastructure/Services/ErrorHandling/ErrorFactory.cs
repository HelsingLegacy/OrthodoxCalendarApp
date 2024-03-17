using CodeBase.Infrastructure.Services.Assets;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.ErrorHandling
{
  public class ErrorFactory : IErrorFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly IErrorWindowProvider _assets;

    public ErrorFactory(IInstantiator instantiator, IErrorWindowProvider assets)
    {
      _instantiator = instantiator;
      _assets = assets;
    }

    public GameObject CreateErrorWindow() => 
      _instantiator.InstantiatePrefab(_assets.ErrorWindow());
  }
}