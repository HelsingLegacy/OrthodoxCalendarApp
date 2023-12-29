using System;

namespace CodeBase.Extensions
{
  public static class NamingExtensions
  {
    private const string JsonExtension = ".json";
    private const string Format = "yyyy-MM-dd";

    public static string Json(this string todayDate) => 
      todayDate + JsonExtension;

    public static string ToStringDateFormat(this DateTime date) => 
      date.ToString(Format);
  }
}