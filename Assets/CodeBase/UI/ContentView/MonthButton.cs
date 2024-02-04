using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI.Presenters;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.ContentView
{
  public class MonthButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
  {
    public MonthListPresenter Presenter;
    public Month Month;
    
    public void OnPointerUp(PointerEventData eventData) => 
      Presenter.Show(Month);

    public void OnPointerDown(PointerEventData eventData)
    {
      
    }
  }
}