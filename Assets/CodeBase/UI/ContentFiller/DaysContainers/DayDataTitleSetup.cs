using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentFiller.DaysContainers
{
  public class DayDataTitleSetup : MonoBehaviour
  {
    [SerializeField] protected Image Background;
    [SerializeField] protected TextMeshProUGUI WeekDay;
    [SerializeField] protected TextMeshProUGUI DateAndMonth;

    protected virtual void Start()
    {
      WeekDay.text = "Четвер";
      DateAndMonth.text = "14 вересня";
      Background.color = new Color(0.4078432f, 0.4470589f, 0.3490196f, 1);
    }

    public virtual void FillWeekDay(string weekDay) => WeekDay.text = weekDay;
    public virtual void FillDateAndMonth(string dateAndMonth) => DateAndMonth.text = dateAndMonth;
    public virtual void FillColorBackground(DayDenotation dayDenotation)
    { 
      var color = Background.color;
      switch (dayDenotation)
      {
        case DayDenotation.Summary:
          color = new Color(0.4078432f, 0.4470589f, 0.3490196f, 1); 
          // ScriptableObject with COLOR scheme for every DataDenotation Cases.
          break;
        case DayDenotation.Prayer:
          color = new Color(0.6039216f, 0.2039216f, 0.1058824f, 1);
          break;
      }
    }
  }
}