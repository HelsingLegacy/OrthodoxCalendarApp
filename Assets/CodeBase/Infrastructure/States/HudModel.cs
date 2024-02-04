using CodeBase.Extensions;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI.Presenters;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
  public class HudModel : MonoBehaviour
  {
    public GameObject GeneralYear;
    public HudMonthPresenter HudMonthPresenter;
    public ContentPresenter ContentPresenter;

    private CalendarFactory _factory;
    private IToday _today;

    [Inject]
    public void Construct(CalendarFactory factory, IToday today)
    {
      _factory = factory;
      _today = today;
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

    public void ShowTodayHoliday()
    {
      PresentersConstructWith(this, _factory); 
      
      ContentPresenter.ShowHoliday(_today.TodayKyiv().ToStringDateFormat());
      HudMonthPresenter.SetMonthName("Лютий");
    }

    public void ClearContent()
    {
      ContentPresenter.CleanUp();
    }

    private void PresentersConstructWith(HudModel model, CalendarFactory factory)
    {
      ContentPresenter.Construct(model, factory);
      HudMonthPresenter.Construct(model, factory);
    }

    public bool Has(Month month)
    {
      switch (month)
      {
        case Month.February:
          //
          return true;
      }

      return false;
    }
  }
}