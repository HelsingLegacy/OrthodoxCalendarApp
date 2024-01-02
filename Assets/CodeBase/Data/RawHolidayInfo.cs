using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeBase.Data
{
  public class RawHolidayInfo
  {
    [JsonProperty("title")] 
    public string Title;

    [JsonProperty("holidayName")] 
    public string HolidayName;

    [JsonProperty("holidayDate")] 
    public DateTime HolidayDate;

    [JsonProperty("weekName")] 
    public string WeekName;

    [JsonProperty("holidayColor")] 
    public string HolidayColor;

    [JsonProperty("liturgyRecommendations")]
    public string LiturgyRecommendations;

    [JsonProperty("content")] 
    public string Content;

    [JsonProperty("mainImage")] 
    public string MainImage;

    [JsonProperty("holidayCategory")] 
    public object HolidayCategory;

    [JsonIgnore]
    public List<HolidayCategory> HolidayCategoryList
    {
      get
      {
        if (HolidayCategory is List<HolidayCategory> categoryList)
          return categoryList;
        return new List<HolidayCategory>();
      }
    }

    [JsonProperty("holidayFast")] 
    public HolidayFast HolidayFast;

    [JsonProperty("holidayFastName")] 
    public HolidayFastName HolidayFastName;

    [JsonProperty("holidaySpecial")] 
    public List<HolidaySpecial> HolidaySpecial;

    [JsonProperty("holidayDress")] 
    public List<HolidayDress> HolidayDress;

    [JsonProperty("dayIcons")] 
    public Dictionary<int, string> DayIcons;
  }

  public class HolidayCategory
  {
    [JsonProperty("slug")] 
    public string Slug;
  }

  public class HolidayFast
  {
    [JsonProperty("slug")] 
    public string Slug;
  }

  public class HolidayFastName
  {
    [JsonProperty("value")] 
    public string Value;
  }

  public class HolidaySpecial
  {
    [JsonProperty("slug")] 
    public string Slug;
  }

  public class HolidayDress
  {
    [JsonProperty("slug")] 
    public string Slug;
  }
}