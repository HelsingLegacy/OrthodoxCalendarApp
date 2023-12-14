using UnityEngine;

namespace CodeBase.Data
{
  public static class Extensions
  {
    public static T ToDeserialize<T>(this string json) => 
      JsonUtility.FromJson<T>(json);
  }
}