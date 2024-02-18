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
    private IConfigProvider _configProvider;

    public string GetTodayDate => _today;


    [Inject]
    public void Construct(CalendarFactory factory, IToday today, IConfigProvider configProvider)
    {
      _factory = factory;
      _today = today.TodayKyivText();
      _configProvider = configProvider;
    }

    public void ShowTodayHoliday()
    {
      ShowHolidayForToday();
      navigation.SetMonthName(_configProvider.GetConfigForToday().Month);
    }

    public void ClearContent() => CleanUpContainer();

    public void ShowHolidayForToday() => 
      _factory.CreateHolidayFullInfo(under: ContentContainer, _today);

    public void ShowHolidayFor(string date) => 
      _factory.CreateHolidayFullInfo(under: ContentContainer, date);

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