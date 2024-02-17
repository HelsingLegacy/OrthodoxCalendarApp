using CodeBase.Data.Services.AssetProviding;
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
    
    private ConfigAssembly _config;

    public ConfigAssembly GetConfig => _config;


    [Inject]
    public void Construct(CalendarFactory factory, IToday today)
    {
      _factory = factory;
      _today = today.TodayKyivText();
    }

    public void ShowTodayHoliday()
    {
      ShowHolidayForToday();
      navigation.SetMonthName(_config.Month);
    }

    public void ClearContent() => CleanUpContainer();

    public void ShowHolidayForToday() => 
      _factory.CreateHolidayFullInfo(under: ContentContainer, _today, out _config);

    public void ShowHolidayFor(string date) => 
      _factory.CreateHolidayFullInfo(under: ContentContainer, date, out _config);

    public string GetCurrentYear() => 
      year.YearText.text;

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