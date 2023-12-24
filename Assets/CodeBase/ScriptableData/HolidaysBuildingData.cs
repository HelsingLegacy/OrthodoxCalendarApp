using UnityEngine;

namespace CodeBase.ScriptableData
{
  [CreateAssetMenu(fileName = "HolidayBuildingData", menuName = "HolidayData", order = 0)]
  public class HolidaysBuildingData : ScriptableObject
  {
    [Header("Red Holiday Color")]
    public Color RedHoliday= new(0.6039216f, 0.2039216f, 0.1058824f, 1);
    public Color BackgroundRedHoliday= new(0.9215687f, 0.8392158f, 0.7843138f, 1);

    [Header("Black Holiday Color")]
    public Color BlackHoliday= new(0.4078432f, 0.4470589f, 0.3490196f, 1);
    public Color BackgroundBlackHoliday= new(0.9803922f, 1f, 0.9490197f, 1);

    [Space][Header("Holiday Category")]
    public Sprite CategoryHoliday;
    public Sprite CategoryVigil;
    public Sprite CategoryTwelfth;
    public Sprite CategoryEaster;
    public Sprite CategoryHymn;
    public Sprite CategoryGlorification;
    public Sprite CategoryHexoChordal;
    
    [Space][Header("Fest")] 
    public Sprite FestUnlimited;
    public Sprite FestFish;
    public Sprite FestOil;
    public Sprite FestStrict;
    public Sprite FestAbstinence;
    
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