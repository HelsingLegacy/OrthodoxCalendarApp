using CodeBase.Infrastructure.Services.TimeDate;

namespace CodeBase.Data.Services.HolidayObserverService
{
  public interface IHolidayDataObserver
  {
    bool Has(Month month, string year);
    bool JsonExistFor(string date);
    bool IconsExistFor(string date);
  }
}