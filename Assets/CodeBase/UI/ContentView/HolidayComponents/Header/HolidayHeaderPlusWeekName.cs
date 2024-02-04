namespace CodeBase.UI.ContentView.HolidayComponents.Header
{
  public class HolidayHeaderPlusWeekName : HolidayHeader
  {
    public void SetWeekName(string text) => GetComponentInChildren<WeekName>().SetWeekName(text);
  }
}