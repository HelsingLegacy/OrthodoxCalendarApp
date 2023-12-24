using CodeBase.UI.ContentFiller.HolidayComponents;
using UnityEngine;

namespace CodeBase.UI.ContentFiller
{
  public class HolidayAssembler : MonoBehaviour
  {
    public GameObject InfoContainer()
    {
      return GetComponentInChildren<InformationContainer>().gameObject;
    }
  }
}