using CodeBase.UI.ContentFillers.NonInteracting.HolidayComponents;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentFillers.NonInteracting
{
  public class HolidayAssembler : MonoBehaviour
  {
    private void Start()
    {
      float currentObjectHeight = 0f;
      
      var providers = GetComponentsInChildren<SizeProvider>();

      foreach (SizeProvider provider in providers) 
        currentObjectHeight += provider.ObjectSize();

      GetComponent<LayoutElement>().preferredHeight = currentObjectHeight;
    }

    public GameObject InfoContainer() => 
      GetComponentInChildren<InformationContainer>().gameObject;

    public void SetBackgroundColor(Color color) => 
      GetComponentInChildren<BackgroundColor>().SetBackground(color);
  }
}