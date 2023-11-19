using UnityEngine;

namespace CodeBase
{
  public class ScreenChecker : MonoBehaviour
  {
    // Start is called before the first frame update
    void Awake()
    {
      Debug.Log(ScreenInchSize());
    }

    private float ScreenInchSize()
    {
      var x = Screen.currentResolution.width / Screen.dpi;
      var y = Screen.currentResolution.height / Screen.dpi;

      var size = (x * x) + (y * y);
      var inchSize = Mathf.Sqrt(size);

      return inchSize;
    }

  }
}