using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI.Presenters;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.ContentFillers.Interacting
{
  public class MonthButton : MonoBehaviour, IPointerClickHandler
  {
    public MonthListPresenter Presenter;
    public Month Month;
    
    public async void OnPointerClick(PointerEventData eventData) => 
      await Presenter.ShowOrDownload(Month);
  }
}