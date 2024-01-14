using CodeBase.UI.ContentFiller.HolidayComponents;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.ContentFiller
{
  public class ReadingAssembler : MonoBehaviour
  {
    private void Start()
    {
      float currentObjectHeight = 0f;
      
      var providers = GetComponentsInChildren<SizeProvider>();

      foreach (SizeProvider provider in providers) 
        currentObjectHeight += provider.ObjectSize();

      GetComponent<LayoutElement>().preferredHeight = currentObjectHeight;
    }

    public void SetReadings(string text) => 
      GetComponentInChildren<ContentWriter>().SetContent(text);
    
    public void SetBackgroundColor(Color color) => 
      GetComponentInChildren<BackgroundColor>().SetBackground(color);

  }
}