using Newtonsoft.Json;

namespace CodeBase.Data.DeserializationClasses
{
  public class HolidayFast
  {
    [JsonProperty("slug")] 
    public string Slug;
  }
}