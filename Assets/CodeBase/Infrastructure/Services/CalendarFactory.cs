using CodeBase.Infrastructure.Services.Assets;
using CodeBase.UI.ContentFiller.HolidayComponents;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services
{
  public class CalendarFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly IAssetProvider _provider;


    public CalendarFactory(IInstantiator instantiator, IAssetProvider provider)
    {
      _instantiator = instantiator;
      _provider = provider;
    }

    public GameObject CreateHud() => 
      _instantiator.InstantiatePrefab(_provider.HudPrefab());

    public GameObject CreateMonthContainer(Transform under) => 
      _instantiator.InstantiatePrefab(_provider.ParticularMonth(), under.transform);

    public GameObject CreateHolidayDataRed(Transform under) => 
      _instantiator.InstantiatePrefab(_provider.HolidayDataRed(), under.transform);

    public GameObject CreateHolidayDataBlack(Transform under) => 
      _instantiator.InstantiatePrefab(_provider.HolidayDataBlack(), under.transform);

    private GameObject CreateSuggestionItem(Transform under) => 
      _instantiator.InstantiatePrefab(_provider.Suggestion(), under.transform);
  }
}