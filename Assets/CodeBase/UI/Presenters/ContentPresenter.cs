using CodeBase.Infrastructure.Services;
using CodeBase.UI.Mediator;
using UnityEngine;

namespace CodeBase.UI.Presenters
{
  public class ContentPresenter : MonoBehaviour
  {
    private HudMediator _mediator;
    private CalendarFactory _factory;
    
    public void Construct(HudMediator mediator, CalendarFactory factory)
    {
      _mediator = mediator;
      _factory = factory;
    }
    
    public void ShowHoliday(string date)
    {
      _factory
        .CreateHolidayFullInfo(gameObject, date);
    }

    public void CleanUp()
    {
      int childCount = transform.childCount;

      for (int i = 0; i < childCount; i++)
      {
        GameObject child = transform.GetChild(i).gameObject;
        
        Destroy(child);
      }
    }
  }
}