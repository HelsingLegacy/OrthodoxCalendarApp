using UnityEngine;

namespace CodeBase.Infrastructure.Services.Assets
{
  public interface IAssetProvider
  {
    GameObject HudPrefab();
    GameObject ParticularMonth();
    GameObject HolidayDataRed();
    GameObject Suggestion();
  }
}