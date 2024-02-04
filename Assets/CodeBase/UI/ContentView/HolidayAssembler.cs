using CodeBase.UI.ContentView.HolidayComponents;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentView
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