using CodeBase.UI.ContentFillers.NonInteracting.HolidayComponents;
using CodeBase.UI.Mediator;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.UI.ContentFillers.NonInteracting
{
  public class ShortHolidayAssembler : BaseHolidayAssembler, IPointerDownHandler, IPointerUpHandler
  {
    private string _date;
    private HudMediator _mediator;

    [Inject]
    public void Construct(HudMediator mediator) => 
      _mediator = mediator;

    public void OnPointerDown(PointerEventData eventData)
    { }

    public void OnPointerUp(PointerEventData eventData)
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