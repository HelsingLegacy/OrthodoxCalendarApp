using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentFillers.NonInteracting
{
  public class BaseHolidayAssembler : MonoBehaviour, IHolidayAssembler
  {
    public void SetBackgroundColor(Color color) => 
      GetComponentInChildren<BackgroundColor>().SetBackground(color);
  }
}