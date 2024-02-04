using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.UI.Presenters
{
  public class ContentPresenter : MonoBehaviour
  {
    private HudModel _model;
    private CalendarFactory _factory;
    
    public void Construct(HudModel model, CalendarFactory factory)
    {
      _model = model;
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