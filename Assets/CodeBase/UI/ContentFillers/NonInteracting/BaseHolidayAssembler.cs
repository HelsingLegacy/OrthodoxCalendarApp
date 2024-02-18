using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentFillers.NonInteracting
{
  public class BaseHolidayAssembler : MonoBehaviour
  {
    public void UpdateSize()
    {
      StartCoroutine(SetSize());
    }
    
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