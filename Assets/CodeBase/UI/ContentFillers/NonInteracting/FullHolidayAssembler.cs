using CodeBase.UI.ContentFillers.NonInteracting.HolidayComponents;
using UnityEngine;

namespace CodeBase.UI.ContentFillers.NonInteracting
{
  public class FullHolidayAssembler : BaseHolidayAssembler
  {
    public GameObject InfoContainer() => 
      GetComponentInChildren<InformationContainer>().gameObject;
  }
}