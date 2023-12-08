using UnityEngine;

namespace CodeBase.Infrastructure.Services.Assets
{
  public class AssetProvider : IAssetProvider
  {
    
    public GameObject HudPrefab() => Load(AssetPath.Hud);

    public GameObject DayData() => Load(AssetPath.DayData);

    public GameObject ParticularMonth() => Load(AssetPath.MonthContainer);

    private GameObject Load(string path) => Resources.Load<GameObject>(path);
  }
}