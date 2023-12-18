using CodeBase.UI;
using CodeBase.UI.ContentFiller.HolidayComponents;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.ScriptableData
{
  [CreateAssetMenu(fileName = "DenotationColorScheme", menuName = "DenotationColorScheme", order = 0)]
  public class DenotationData : ScriptableObject
  {
    [SerializeField] public DayDenotation Denotation;
    
    [Header("Color Scheme")]
    [SerializeField] public Color32 DayTitleColor;
    [SerializeField] public Color32 DayBackgroundColor;
    
    [Header("Text Settings")]
    [SerializeField] public TMP_FontAsset TextFont;
  }
}