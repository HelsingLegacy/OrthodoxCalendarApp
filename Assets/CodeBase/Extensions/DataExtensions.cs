using System.Text.RegularExpressions;
using CodeBase.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace CodeBase.Extensions
{
  public static class DataExtensions
  {
    public static T ToDeserialize<T>(this string json) =>
      JsonConvert.DeserializeObject<T>(json);

    public static string RemoveUnnecessaryEscape(this string json) =>
      json.Replace(@"\/", "/");

    public static string RemoveHtmlTags(this string json) =>
      Regex.Replace(json, @"<[^>]*>", "");

    public static string RemoveNewLineInBeginning(this string json) => 
      json.StartsWith("\n") ? json.Substring(1) : json;
  }
}