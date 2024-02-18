using CodeBase.UI.ContentFillers.NonInteracting.HolidayComponents;
using UnityEngine;

namespace CodeBase.UI.ContentFillers.NonInteracting
{
  public class ReadingAssembler : BaseHolidayAssembler
  {
    public void SetReadings(string text) => 
      GetComponentInChildren<ContentWriter>().SetContent(text);
    
    public void SetBackgroundColor(Color color) => 
      GetComponentInChildren<BackgroundColor>().SetBackground(color);

  }
}