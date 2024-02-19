namespace CodeBase.Data.Services.AssetProviding
{
  public interface IHolidaysStorage
  {
    void BindDataPath();
    string HolidayConfigFor(string date);
    string HolidayIconFor(string date);
  }
}