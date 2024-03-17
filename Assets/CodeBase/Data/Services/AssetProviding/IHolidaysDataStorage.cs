namespace CodeBase.Data.Services.AssetProviding
{
  public interface IHolidaysDataStorage
  {
    string HolidayConfigFor(string date);
    string HolidayIconFor(string date);
  }
}