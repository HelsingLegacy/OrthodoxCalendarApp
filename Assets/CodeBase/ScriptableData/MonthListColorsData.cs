using UnityEngine;

namespace CodeBase.ScriptableData
{
  [CreateAssetMenu(fileName = "MonthList", menuName = "ScriptableData/ColorSettings", order = 1)]
  public class MonthListColorsData : ScriptableObject
  {
    [field: SerializeField] public Color Available = new(0.91f, 0.93f, 0.75f, 1);
    [field: SerializeField] public Color Unavailable = new(0.45f, 0.45f, 0.45f, 1);
  }
}