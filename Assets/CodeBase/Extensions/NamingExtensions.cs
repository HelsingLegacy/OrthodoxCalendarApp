using System;

namespace CodeBase.Extensions
{
  public static class NamingExtensions
  {
    private const string ReadingPrefix = "-reading";
    private const string JsonExtension = ".json";
    private const string Format = "yyyy-MM-dd";

    public static string Reading(this string todayDate) => 
      todayDate + ReadingPrefix;
    
    public static string Json(this string todayDate) => 
      todayDate + JsonExtension;

    public static string ToDateFormat(this DateTime date) => 
      date.ToString(Format);
  }
}