namespace CodeBase.UI.ContentFiller.HolidayComponents.Header
{
  public class HolidayHeaderPlusWeekName : HolidayHeader
  {
    public void SetWeekName(string text) => GetComponentInChildren<WeekName>().SetWeekName(text);
  }
}