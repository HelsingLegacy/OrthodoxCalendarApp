using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI.Presenters;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.UI.ContentHandlers.Interacting
{
  public class MonthButton : MonoBehaviour, IPointerClickHandler
  {
    public MonthListController controller;
    public Month Month;
    
    public void OnPointerClick(PointerEventData eventData) => 
      controller.ShowOrDownload(Month).Forget();

    public void SetColor(Color color) => 
      GetComponent<Image>().color = color;
  }
}