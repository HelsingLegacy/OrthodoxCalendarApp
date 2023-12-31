using CodeBase.Data.Services.JsonHandle;
using CodeBase.Infrastructure.Services;
using CodeBase.UI;
using CodeBase.UI.ContentFiller;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LoadCalendarState : IState
  {
    private readonly LoadingCurtain _curtain;
    private readonly CalendarFactory _factory;
    private readonly IStateMover _resolver;
    private readonly HolidayDataExtractor _extractor;

    public LoadCalendarState(LoadingCurtain curtain, CalendarFactory factory, IStateMover resolver, HolidayDataExtractor extractor)
    {
      _curtain = curtain;
      _factory = factory;
      _resolver = resolver;
      _extractor = extractor;
    }

    public void Enter()
    {
      InitCalendar();
      _curtain.Hide();
    }

    public void Exit()
    {
    }

    private void InitCalendar()
    {
      TestUIElementsCreation();

      _resolver.MoveTo<UserObservationState>();
    }

    private void TestUIElementsCreation()
    {
      GameObject hud =
        _factory
          .CreateHud()
          .GetComponent<ParentProvider>()
          .ParentObject();

      GameObject monthParent =
        _factory
          .CreateMonthContainer(under: hud.transform)
          .GetComponent<ParentProvider>()
          .ParentObject();

      //_factory.CreateHolidayDataAssembly(monthParent.transform);
    }
  }
}