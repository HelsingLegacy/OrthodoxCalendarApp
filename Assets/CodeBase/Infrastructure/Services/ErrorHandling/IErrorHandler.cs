using System;
using System.Collections;

namespace CodeBase.Infrastructure.Services.ErrorHandling
{
  public interface IErrorHandler
  {
    void PopupError();
    IEnumerator ResetError(Action onLoaded = null);
  }
}