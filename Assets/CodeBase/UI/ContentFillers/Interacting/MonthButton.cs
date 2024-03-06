using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI.Presenters;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.ContentFillers.Interacting
{
  public class MonthButton : MonoBehaviour, IPointerClickHandler
  {
    public MonthListController controller;
    public Month Month;
    
    public void OnPointerClick(PointerEventData eventData) => 
      controller.ShowOrDownload(Month).Forget();
  }
}