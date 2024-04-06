using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI.Presenters;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.ContentHandlers.Interacting
{
  public class MonthButton : MonoBehaviour, IPointerClickHandler
  {
    public MonthListController controller;
    public Month Month;
    
    private LoadingCurtain _curtain;

    private bool _isActive; 

    [Inject]
    public void Construct(LoadingCurtain curtain) => 
      _curtain = curtain;

    public void OnPointerClick(PointerEventData eventData)
    {
      if(!_isActive)
        return;
      
      _curtain.Show();

      controller.ShowOrDownload(Month).Forget();
    }

    public void SetState(bool state, Color color)
    {
      _isActive = state;
      GetComponent<Image>().color = color;
    }
  }
}