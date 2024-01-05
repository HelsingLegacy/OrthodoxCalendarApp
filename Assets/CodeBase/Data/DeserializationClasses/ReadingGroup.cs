using Newtonsoft.Json;

namespace CodeBase.Data.DeserializationClasses
{
  public class ReadingGroup
  {
    [JsonProperty("holiday_reading_group_title")]
    public string Title;

    [JsonProperty("holiday_reading_group_code")]
    public string Code;

    [JsonProperty("holiday_reading_group_request")]
    public string Request;

    [JsonProperty("holiday_reading_group_text")]
    public string Text;

    [JsonProperty("holiday_reading_copyright")]
    public string Copyright;
  }
}