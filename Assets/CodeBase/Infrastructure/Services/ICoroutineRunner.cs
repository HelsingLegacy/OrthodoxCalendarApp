using System.Collections;
using CodeBase.Data.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator load);
  }
}