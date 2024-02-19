using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentFillers.NonInteracting
{
  public class BaseHolidayAssembler : MonoBehaviour, IHolidayAssembler
  {
    public void UpdateSize()
    {
      StartCoroutine(SetSize());
    }
    
    public void SetBackgroundColor(Color color) => 
      GetComponentInChildren<BackgroundColor>().SetBackground(color);
    
    private IEnumerator SetSize()
    {
      yield return new WaitForFixedUpdate();
      
      float currentObjectHeight = 0f;
      
      var providers = GetComponentsInChildren<SizeProvider>();

      foreach (SizeProvider provider in providers) 
        currentObjectHeight += provider.ObjectSize();

      GetComponent<LayoutElement>().preferredHeight = currentObjectHeight;
    }
  }
}