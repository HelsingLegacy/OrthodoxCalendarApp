using TMPro;
using UnityEngine;

namespace CodeBase.UI.ContentFiller.HolidayComponents
{
  public class HolidaySetupBlack : MonoBehaviour
  {
    [Header("HolidayHeader")]
    [SerializeField] private TextMeshProUGUI WeekDay;
    [SerializeField] private TextMeshProUGUI DateAndMonth;

    [Space] 
    [SerializeField] private GameObject Suggestions;
    
    [Space]
    [SerializeField] private TextMeshProUGUI Introduction;
    
    public void FillWeekDay(string weekDay) => WeekDay.text = weekDay;
    
    public void FillDayAndMonth(string dayAndMonth) => DateAndMonth.text = dayAndMonth;

    public Transform FillSuggestions() => Suggestions.transform;
    
    public void FillIntroduction(string introduction) => Introduction.text = introduction;
  }
}