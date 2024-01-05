using Newtonsoft.Json;

namespace CodeBase.Data.DeserializationClasses
{
  public class HolidaySpecial
  {
    [JsonProperty("slug")] 
    public string Slug;
  }
}