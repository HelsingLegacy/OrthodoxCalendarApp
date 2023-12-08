using CodeBase.Infrastructure.Services.Assets;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services
{
  public class CalendarFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly AssetProvider _provider;


    public CalendarFactory(IInstantiator instantiator, AssetProvider provider)
    {
      _instantiator = instantiator;
      _provider = provider;
    }

    public GameObject CreateHud() => 
      _instantiator.InstantiatePrefab(_provider.HudPrefab());

    public GameObject CreateDayData(Transform under) => 
      _instantiator.InstantiatePrefab(_provider.DayData(), under.transform);

    public GameObject CreateMonthContainer(Transform under) => 
      _instantiator.InstantiatePrefab(_provider.ParticularMonth(), under.transform);
  }
}