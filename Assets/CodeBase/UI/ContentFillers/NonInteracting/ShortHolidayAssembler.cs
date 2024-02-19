using CodeBase.UI.ContentFillers.NonInteracting.HolidayComponents;
using CodeBase.UI.Mediator;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.UI.ContentFillers.NonInteracting
{
  public class ShortHolidayAssembler : BaseHolidayAssembler, IPointerClickHandler
  {
    private string _date;
    private HudMediator _mediator;
    private bool _pointerDown;

    [Inject]
    public void Construct(HudMediator mediator) => 
      _mediator = mediator;
    
    public void OnPointerClick(PointerEventData eventData)
    {
      _mediator.ShowCurtainWithContentCleanup();
      _mediator.ShowHolidayFor(_date);
      _mediator.HideCurtain();
    }
    
    public void SetDate(string date) => 
      _date = date;

    public GameObject InfoContainer() => 
      GetComponentInChildren<InformationContainer>().gameObject;
  }
}