using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.TimeDate;
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
    private IMonthName _label;

    [Inject]
    public void Construct(LoadingCurtain curtain, CalendarFactory factory, IMonthName month)
    {
      _curtain = curtain;
      _factory = factory;
      _label = month;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (_isTodayDisplay)
        ShowTodayFullInfo();
      else
        ShowMonthList();
    }

    public void SetMainNavigationName(string label = null) =>
      Text.text = label ?? _label.CurrentMonth();

    private void ShowTodayFullInfo()
    {
      _curtain.Show();
      Mediator.ResetAndCleanupContent();
      
      SetMainNavigationName();

      _factory.CreateHolidayFullInfo(Mediator.ContentContainer, Mediator.GetTodayDate);
      _isTodayDisplay = false;

      _curtain.HideWithDelay();
    }

    private void ShowMonthList()
    {
      _curtain.Show();
      Mediator.ResetAndCleanupContent();

      SetMainNavigationName(TodayText);
      
      InitMonthList();

      _isTodayDisplay = true;
      
      _curtain.HideWithDelay();
    }

    private void InitMonthList()
    {
      GameObject monthList = _factory.CreateMonthList(parent: Mediator.ContentContainer);
      monthList.GetComponent<MonthListController>().ActivateButtons();
    }
  }
}