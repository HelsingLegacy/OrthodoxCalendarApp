using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodeBase.Data.DeserializationClasses
{
  public class HolidayCategoryConverter : JsonConverter
  {
    public override bool CanConvert(Type objectType) => 
      objectType == typeof(List<HolidayCategory>);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      JToken token = JToken.Load(reader);

      if (token.Type == JTokenType.Boolean)
      {
        return new List<HolidayCategory>();
      }

      return token.ToObject<List<HolidayCategory>>();
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer){}
  }
}