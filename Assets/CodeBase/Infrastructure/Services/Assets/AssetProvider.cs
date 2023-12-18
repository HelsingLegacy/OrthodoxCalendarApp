using UnityEngine;

namespace CodeBase.Infrastructure.Services.Assets
{
  public class AssetProvider : IAssetProvider
  {
    public GameObject HudPrefab() => Load(AssetPath.Hud);
    
    public GameObject ParticularMonth() => Load(AssetPath.MonthContainer);

    public GameObject HolidayDataRed() => Load(AssetPath.HolidayDataRed);
    
    public GameObject HolidayDataBlack() => Load(AssetPath.HolidayDataBlack);

    public GameObject Suggestion() => Load(AssetPath.Suggestion);

    private GameObject Load(string path) => Resources.Load<GameObject>(path);
  }
}