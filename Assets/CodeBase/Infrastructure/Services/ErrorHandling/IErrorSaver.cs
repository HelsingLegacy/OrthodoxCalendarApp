namespace CodeBase.Infrastructure.Services.ErrorHandling
{
  public interface IErrorSaver
  {
    void SetErrorCode(ErrorID id);
  }
}