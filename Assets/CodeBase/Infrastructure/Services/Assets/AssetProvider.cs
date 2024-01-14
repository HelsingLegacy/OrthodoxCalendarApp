using UnityEngine;

namespace CodeBase.Infrastructure.Services.Assets
{
  public class AssetProvider : IAssetProvider
  {
    public GameObject HudPrefab() => Load(AssetPath.Hud);
    
    public GameObject ParticularMonth() => Load(AssetPath.MonthContainer);

    public GameObject HolidayDataAssembly() => Load(AssetPath.HolidayDataAssembly);
    
    public GameObject HeaderNoName() => Load(AssetPath.HeaderNoName);
    public GameObject HeaderWithName() => Load(AssetPath.HeaderWithName);
    
    public GameObject HolidayName() => Load(AssetPath.HolidayName);
    
    public GameObject IconImage() => Load(AssetPath.IconImage);
    
    public GameObject Suggestion() => Load(AssetPath.Suggestion);
    public GameObject SuggestionItem() => Load(AssetPath.SuggestionItem);
    
    public GameObject GeneralContentText() => Load(AssetPath.GeneralContentText);
    public GameObject HolidayReadings() => Load(AssetPath.HolidayReadings);

    public GameObject DayIconsContainer() => Load(AssetPath.DayIconsContainer);

    private GameObject Load(string path) => Resources.Load<GameObject>(path);
  }
}