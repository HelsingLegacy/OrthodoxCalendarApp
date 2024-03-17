using CodeBase.UI.ContentHandlers.NonInteracting;
using CodeBase.UI.ContentHandlers.NonInteracting.HolidayComponents;
using CodeBase.UI.Mediator;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.UI.ContentHandlers.Interacting
{
  public class ShortHolidayAssembly : BaseHolidayAssembler, IPointerClickHandler
  {
    private string _date;
    private MainWindow _mediator;
    private bool _pointerDown;
    private LoadingCurtain _curtain;

    [Inject]
    public void Construct(MainWindow mediator, LoadingCurtain curtain)
    {
      _mediator = mediator;
      _curtain = curtain;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      _curtain.Show();
      _mediator.ResetAndCleanupContent();

      _mediator.ShowHolidayFor(_date);
      
      _curtain.HideWithDelay();
    }
    
    public void SetDate(string date) => 
      _date = date;

    public GameObject InfoContainer() => 
      GetComponentInChildren<InformationContainer>().gameObject;
  }
}