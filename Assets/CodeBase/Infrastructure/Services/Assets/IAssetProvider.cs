using UnityEngine;

namespace CodeBase.Infrastructure.Services.Assets
{
  public interface IAssetProvider
  {
    GameObject DayData();
    GameObject HudPrefab();
    GameObject ParticularMonth();
  }
}