using CodeBase.UI.Presenters;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.ContentFillers.Interacting
{
  public class MonthListButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
  {
    public HudMonthPresenter ListMonthBase;
    
    public void OnPointerUp(PointerEventData eventData) => 
      ListMonthBase.ShowMonthList();

    public void OnPointerDown(PointerEventData eventData)
    {
      
    }
  }
}