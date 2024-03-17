using UnityEngine;

namespace CodeBase.Infrastructure.Services.ErrorHandling
{
  public interface IErrorFactory
  {
    GameObject CreateErrorWindow();
  }
}