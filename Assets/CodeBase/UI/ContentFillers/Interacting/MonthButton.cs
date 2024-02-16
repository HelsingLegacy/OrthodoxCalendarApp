using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI.Presenters;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.ContentFillers.Interacting
{
  public class MonthButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
  {
    public MonthListPresenter Presenter;
    public Month Month;
    
    public async void OnPointerUp(PointerEventData eventData) => 
      await Presenter.ShowOrDownload(Month);

    public void OnPointerDown(PointerEventData eventData) { }
  }
}