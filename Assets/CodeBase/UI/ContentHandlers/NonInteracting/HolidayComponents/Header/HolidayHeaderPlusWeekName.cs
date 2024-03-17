namespace CodeBase.UI.ContentHandlers.NonInteracting.HolidayComponents.Header
{
  public class HolidayHeaderPlusWeekName : HolidayHeader
  {
    public void SetWeekName(string text) => GetComponentInChildren<WeekName>().SetWeekName(text);
  }
}