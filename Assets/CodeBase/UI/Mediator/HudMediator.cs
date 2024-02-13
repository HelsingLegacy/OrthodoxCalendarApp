using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI.Presenters;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Mediator
{
  public class HudMediator : MonoBehaviour
  {
    public GameObject YearButton;
    public MainNavigationButton NavigationButton;
    
    public GameObject ContentContainer;

    private CalendarFactory _factory;
    private string _today;

    [Inject]
    public void Construct(CalendarFactory factory, IToday today)
    {
      _factory = factory;
      _today = today.TodayKyivText();
    }

    public void ShowTodayHoliday()
    {
      ShowHolidayForToday();
      NavigationButton.SetMonthName("Лютий");
    }

    public void ClearContent() => CleanUpContainer();

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

    public void ShowHolidayForToday() => _factory.CreateHolidayFullInfo(ContentContainer, _today);

    public void ShowHoliday(string date) => _factory.CreateHolidayFullInfo(ContentContainer, date);

    private void Download(Month month)
    {
      
    }

    private void CleanUpContainer()
    {
      int childCount = ContentContainer.transform.childCount;

      for (int i = 0; i < childCount; i++)
      {
        GameObject child = ContentContainer.transform.GetChild(i).gameObject;
        
        Destroy(child);
      }
    }

  }
}