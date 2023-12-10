using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.JsonLoader;
using CodeBase.UI;

namespace CodeBase.Infrastructure.States
{
  public class WarmUpState : IState
  {
    private const string Main = "Main";
    
    private readonly ISceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IStateMover _resolver;
    private readonly JsonSaver _jsonSaver;

    public WarmUpState(ISceneLoader sceneLoader, LoadingCurtain curtain, IStateMover resolver, JsonSaver jsonSaver)
    {
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _resolver = resolver;
      _jsonSaver = jsonSaver;
    }

    public void Enter()
    {
      _curtain.Show();
      _jsonSaver.LoadJsonFromServer();
      _sceneLoader.LoadScene(Main, EnterLoadCalendar);
    }

    public void Exit()
    {
    }

    private void EnterLoadCalendar() => 
      _resolver.MoveTo<LoadCalendarState>();
  }
}
