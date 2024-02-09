namespace CodeBase.UI.ContentFillers.NonInteracting.HolidayComponents.Header
{
  public class HolidayHeaderPlusWeekName : HolidayHeader
  {
    public void SetWeekName(string text) => GetComponentInChildren<WeekName>().SetWeekName(text);
  }
}