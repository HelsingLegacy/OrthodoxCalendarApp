namespace CodeBase.Data.Services
{
  public interface IJsonSaver
  {
    void LoadJsonFor(string dateParameter = JsonSaver.TodayParameter);
  }
}