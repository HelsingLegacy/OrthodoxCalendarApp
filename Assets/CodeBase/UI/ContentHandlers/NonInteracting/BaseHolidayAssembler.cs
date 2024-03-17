using UnityEngine;

namespace CodeBase.UI.ContentHandlers.NonInteracting
{
  public class BaseHolidayAssembler : MonoBehaviour, IHolidayAssembler
  {
    public void SetBackgroundColor(Color color) => 
      GetComponentInChildren<BackgroundColor>().SetBackground(color);
  }
}