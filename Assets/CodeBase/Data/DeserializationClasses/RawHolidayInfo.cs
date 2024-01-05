using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodeBase.Data.DeserializationClasses
{
  public class RawHolidayInfo
  {
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
    [JsonConverter(typeof(HolidayCategoryConverter))]
    public List<HolidayCategory> HolidayCategory;

    [JsonProperty("holidayFast")] 
    public HolidayFast HolidayFast;

    [JsonProperty("holidayFastName")] 
    public HolidayFastName HolidayFastName;

    [JsonProperty("holidaySpecial")] 
    public List<HolidaySpecial> HolidaySpecial;

    [JsonProperty("holidayDress")] 
    public List<HolidayDress> HolidayDress;

    [JsonProperty("readingGroup")] 
    public List<ReadingGroup> ReadingGroupList;

    [JsonProperty("dayIcons")] 
    public Dictionary<int, string> DayIcons;
  }
}