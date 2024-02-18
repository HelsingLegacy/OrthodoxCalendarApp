using CodeBase.Infrastructure.Services;
using CodeBase.UI.Mediator;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CodeBase.UI.Presenters
{
  public class MainNavigation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
  {
    private const string TodayText = "Сьогодні";
    private bool _isTodayDisplay;
    
    public TextMeshProUGUI Text;
    public HudMediator Mediator;
    
    private CalendarFactory _factory;
    private IConfigProvider _configProvider;

    [Inject]
    public void Construct(CalendarFactory factory, IConfigProvider configProvider)
    {
      _factory = factory;
      _configProvider = configProvider;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
      if (_isTodayDisplay)
      {
        SetMonthName(_configProvider.GetConfigForToday().Month);
        Mediator.ClearContent();
        _factory.CreateHolidayFullInfo(Mediator.ContentContainer, Mediator.GetTodayDate);
        _isTodayDisplay = false;
      }
      else
      {
        ShowMonthList();
        
        SetMonthName(TodayText);
        _isTodayDisplay = true;
      }
    }

    public void OnPointerDown(PointerEventData eventData) { }

    public void SetMonthName(string month) =>
      Text.text = month;

    private void ShowMonthList()
    {
      Mediator.ClearContent();
      
      _factory.CreateMonthList(parent: Mediator.ContentContainer);
    }
  }
}