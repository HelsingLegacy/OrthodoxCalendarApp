using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI.Presenters;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Mediator
{
  public class HudMediator : MonoBehaviour
  {
    public YearNavigation year;
    public MainNavigation navigation;
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
      navigation.SetMonthName("Лютий");
    }

    public void ClearContent() => CleanUpContainer();

    public bool Has(Month month, string year)
    {
      switch (month)
      {
        case Month.February:
          
          return true;
      }

      return false;
    }

    public void ShowHolidayForToday() => _factory.CreateHolidayFullInfo(ContentContainer, _today);

    public void ShowHoliday(string date) => _factory.CreateHolidayFullInfo(ContentContainer, date);

    public string GetCurrentYear() => year.YearText.text;

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