using CodeBase.Infrastructure.Services.TimeDate;

namespace CodeBase.Data.Services.HolidayObserverService
{
  public interface IHolidayObserver
  {
    bool Has(Month month, string year);
    bool RequestedFileExistFor(string date);
  }
}