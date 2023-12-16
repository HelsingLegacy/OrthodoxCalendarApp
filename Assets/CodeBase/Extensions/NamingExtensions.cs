namespace CodeBase.Extensions
{
  public static class NamingExtensions
  {
    private const string ReadingPrefix = "-reading";
    private const string JsonExtension = ".json";

    public static string Reading(this string todayDate) => 
      todayDate + ReadingPrefix;
    public static string Json(this string todayDate) => 
      todayDate + JsonExtension;
    
  }
}