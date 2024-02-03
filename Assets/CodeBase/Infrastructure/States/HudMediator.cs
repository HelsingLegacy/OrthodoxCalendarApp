using CodeBase.UI;
using CodeBase.UI.ViewComponents;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
  public class HudMediator : MonoBehaviour
  {
    private void Start()
    {
      PresentersConstructWith(this);
    }

    [Inject]
    public void Construct()
    {
      
    }
    
    public void Download(Month month)
    {
      
    }

    public void ShiftMediatorParent()
    {
      HudParent parent = FindObjectOfType<HudParent>();
      
      gameObject.GetComponent<RectTransform>().SetParent(parent.RectParent());
      gameObject.GetComponent<RectTransform>().SetParent(new RectTransform());
      
      Destroy(parent.gameObject);
    }

    private void PresentersConstructWith(HudMediator mediator)
    {
      var presenters = FindObjectsOfType<Presenter>();
      
      foreach (Presenter presenter in presenters) 
        presenter.Construct(mediator);
    }
  }
}