namespace CodeBase.UI.ContentFiller.HolidayComponents.Header
{
  public class SetupHeaderPlusWeekName : SetupHeader
  {
    public void SetWeekName(string text) => GetComponentInChildren<WeekName>().SetWeekName(text);
  }
}