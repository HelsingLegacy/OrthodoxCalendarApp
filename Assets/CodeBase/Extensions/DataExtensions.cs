using UnityEngine;

namespace CodeBase.Extensions
{
  public static class DataExtensions
  {
    public static T ToDeserialize<T>(this string json) => 
      JsonUtility.FromJson<T>(json);

    public static string RemoveUnnecessaryEscape(this string json) => 
      json.Replace(@"\/", "/");
  }
}