using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services
{
  public class CalendarFactory
  {
    private readonly IInstantiator _instantiator;

    
    public CalendarFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public GameObject CreateHud()
    {
      GameObject prefab = Resources.Load<GameObject>("Prefabs/HUD");
      return _instantiator.InstantiatePrefab(prefab);
    }

    public GameObject CreateDayData(Transform under)
    {
      GameObject prefab = Resources.Load<GameObject>("Prefabs/DayData");
      return _instantiator.InstantiatePrefab(prefab, under.transform);
    }

    public GameObject CreateMonthContainer(Transform under)
    {
      GameObject prefab = Resources.Load<GameObject>("Prefabs/MonthContainer");
      return _instantiator.InstantiatePrefab(prefab, under.transform);
    }
  }

}