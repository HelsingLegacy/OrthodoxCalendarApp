using UnityEngine;

namespace CodeBase.Infrastructure.Services.Assets
{
  public interface IAssetProvider
  {
    GameObject HudPrefab();
    
    GameObject FullHolidayAssembly();
    GameObject ShortHolidayAssembly();

    GameObject HeaderNoName();
    GameObject HeaderWithName();

    GameObject IconImage();

    GameObject HolidayName();

    GameObject Suggestion();
    GameObject SuggestionItem();

    GameObject GeneralContentText();

    GameObject HolidayReadings();
    
    GameObject DayIconsContainer();
    GameObject MonthList();
    
    GameObject ProgressBar();
  }
}