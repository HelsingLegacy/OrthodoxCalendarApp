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
    
    [Inject]
    public void Construct(CalendarFactory factory)
    {
      _factory = factory;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
      if (_isTodayDisplay)
      {
        _isTodayDisplay = false;
      }
      else
      {
        ShowMonthList();
      }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      
    }

    public void SetMonthName(string month) =>
      Text.text = month;

    private void ShowMonthList()
    {
      Mediator.ClearContent();
      _factory.CreateMonthList(Mediator);
      SetMonthName(TodayText);
      _isTodayDisplay = true;
    }
  }
}