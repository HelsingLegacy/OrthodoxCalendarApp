namespace CodeBase.Data.Services.AssetProviding
{
  public class LinkProvider : ILinkProvider
  {
    private const string LinkBody = "https://orthodox-calendar.com.ua/wp-json/calendar/v1/holiday/";
    private const string Parameter = "/?recommendations=true&reading=true";

    public string HolidayLink() => LinkBody;
    public string ReadingParameter() => Parameter;
    
  }
}