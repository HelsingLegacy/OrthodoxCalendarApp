using CodeBase.UI.ContentHandlers.NonInteracting;
using CodeBase.UI.ContentHandlers.NonInteracting.HolidayComponents;
using UnityEngine;

namespace CodeBase.UI.ContentHandlers.Interacting
{
  public class FullHolidayAssembler : BaseHolidayAssembler
  {
    public GameObject InfoContainer() => 
      GetComponentInChildren<InformationContainer>().gameObject;
  }
}