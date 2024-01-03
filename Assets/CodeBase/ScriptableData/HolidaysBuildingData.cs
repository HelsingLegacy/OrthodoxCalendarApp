using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.ScriptableData
{
  [CreateAssetMenu(fileName = "HolidayBuildingData", menuName = "HolidayData", order = 0)]
  public class HolidaysBuildingData : ScriptableObject
  {
    [Header("Red Holiday Color")]
    public Color HeaderRed= new(0.6039216f, 0.2039216f, 0.1058824f, 1);
    public Color BackgroundRed= new(0.9215687f, 0.8392158f, 0.7843138f, 1);

    [FormerlySerializedAs("BlackHoliday")] [Header("Black Holiday Color")]
    public Color HeaderBlack= new(0.4078432f, 0.4470589f, 0.3490196f, 1);
    public Color BackgroundBlack= new(0.9803922f, 1f, 0.9490197f, 1);

    /// <summary>
    /// Велике свято
    /// </summary>
    [Space][Header("Holiday Category")]
    public Sprite CategoryHoliday;
    
    /// <summary>
    /// Бдіння
    /// </summary>
    public Sprite CategoryVigil; 
    
    /// <summary>
    /// Двунадесяте
    /// </summary>
    public Sprite CategoryTwelfth;
    
    /// <summary>
    /// Пасха
    /// </summary>
    public Sprite CategoryEaster;
    
    /// <summary>
    /// Полієлей
    /// </summary>
    public Sprite CategoryHymn;
    
    /// <summary>
    /// Славословіє
    /// </summary>
    public Sprite CategoryGlorification;
    
    /// <summary>
    /// Шестерична
    /// </summary>
    public Sprite CategoryHexoChordal;
    
    [Space][Header("Fast")] 
    public Sprite FastUnlimited;
    public Sprite FastFish;
    public Sprite FastOil;
    public Sprite FastStrict;
    public Sprite FastAbstinence;
    
    [Space][Header("Holiday Special")]
    public Sprite Funeral;
    
    [Space][Header("Holiday Dress")]
    public Sprite DressBurgundy;
    public Sprite DressRed;
    public Sprite DressYellow;
    public Sprite DressGreen;
    public Sprite DressBlue;
    public Sprite DressWhite;
    public Sprite DressViolet;




    
    
  }
}