using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
  [Serializable]
  public class HolidaysData
  {
    public int postId;
    public string title;
    public string holidayName;
    public string holidayDate;
    public string weekName;
    public string holidayColor;
    public string liturgyRecommendations;
    public string content;
    public string mainImage;
    public bool holidayCategory;
    public string holidayFast;
    public string holidaySpecial;
    public List<string> holidayDress;
    public string dayIcons;

    public List<ReadingGroup> readingGroup;

    [System.Serializable]
    public class ReadingGroup
    {
      public string holiday_reading_group_title;
      public string holiday_reading_group_code;
      public string holiday_reading_group_request;
      public string holiday_reading_group_text;
      public string holiday_reading_copyright;
    }
  }
}