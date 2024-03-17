namespace CodeBase.Infrastructure.Services.ErrorHandling
{
  public interface IErrorStateProvider
  {
    bool IsAnError();
    bool IsNoError();
  }
}