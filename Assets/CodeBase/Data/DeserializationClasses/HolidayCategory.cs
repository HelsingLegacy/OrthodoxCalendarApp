using Newtonsoft.Json;

namespace CodeBase.Data.DeserializationClasses
{
  public class HolidayCategory
  {
    [JsonProperty("slug")] 
    public string Slug;
  }
}