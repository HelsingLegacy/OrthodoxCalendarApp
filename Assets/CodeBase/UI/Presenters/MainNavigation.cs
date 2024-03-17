using CodeBase.Data.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.UI.Mediator;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.UI.Presenters
{
  public class MainNavigation : MonoBehaviour, IPointerClickHandler
  {
    public TextMeshProUGUI Text;
    public MainWindow Mediator;
    
    private const string TodayText = "Сьогодні";
    private bool _isTodayDisplay;

    private LoadingCurtain _curtain;
    private CalendarFactory _factory;
    private IConfigProvider _configProvider;

    [Inject]
    public void Construct(LoadingCurtain curtain, CalendarFactory factory, IConfigProvider configProvider)
    {
      _curtain = curtain;
      _factory = factory;
      _configProvider = configProvider;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (_isTodayDisplay)
        ShowTodayFullInfo();
      else
        ShowMonthList();
    }

    public void SetMainNavigationName(string month) =>
      Text.text = month;

    private void ShowTodayFullInfo()
    {
      _curtain.Show();
      Mediator.ResetAndCleanupContent();
      
      SetMainNavigationName(_configProvider.GetConfigForToday().Month);

      _factory.CreateHolidayFullInfo(Mediator.ContentContainer, Mediator.GetTodayDate);
      _isTodayDisplay = false;

      _curtain.HideWithDelay();
    }

    private void ShowMonthList()
    {
      _curtain.Show();
      Mediator.ResetAndCleanupContent();

      SetMainNavigationName(TodayText);
      
      _factory.CreateMonthList(parent: Mediator.ContentContainer);
      _isTodayDisplay = true;
      
      _curtain.HideWithDelay();
    }
  }
}