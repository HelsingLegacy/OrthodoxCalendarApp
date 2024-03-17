using CodeBase.UI.ContentHandlers.NonInteracting.HolidayComponents;

namespace CodeBase.UI.ContentHandlers.NonInteracting
{
  public class ReadingAssembler : BaseHolidayAssembler
  {
    public void SetReadings(string text) => 
      GetComponentInChildren<ContentWriter>().SetContent(text);
  }
}