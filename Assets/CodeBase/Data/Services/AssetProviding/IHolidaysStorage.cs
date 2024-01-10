namespace CodeBase.Data.Services.AssetProviding
{
  public interface IHolidaysStorage
  {
    string HolidayConfigFor(string date);
    string HolidayIconFor(string date);
  }
}