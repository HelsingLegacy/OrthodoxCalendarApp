using CodeBase.UI.ContentFillers.NonInteracting.HolidayComponents;
using UnityEngine;

namespace CodeBase.UI.ContentFillers.NonInteracting
{
  public class HolidayAssembler : BaseHolidayAssembler
  {
    public GameObject InfoContainer() => 
      GetComponentInChildren<InformationContainer>().gameObject;

    public void SetBackgroundColor(Color color) => 
      GetComponentInChildren<BackgroundColor>().SetBackground(color);
  }
}