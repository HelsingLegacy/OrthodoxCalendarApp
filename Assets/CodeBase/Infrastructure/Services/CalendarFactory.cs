using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.UI.ContentFiller;
using CodeBase.UI.ContentFiller.HolidayComponents;
using CodeBase.UI.ContentFiller.HolidayComponents.Header;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services
{
  public class CalendarFactory
  {
    private readonly IInstantiator _instantiator;
    private readonly IAssetProvider _provider;
    private readonly ClearData _clearData;


    public CalendarFactory(IInstantiator instantiator, IAssetProvider provider, ClearData clearData)
    {
      _instantiator = instantiator;
      _provider = provider;
      _clearData = clearData;
    }

    public GameObject CreateHud() =>
      _instantiator.InstantiatePrefab(_provider.HudPrefab());

    public GameObject CreateMonthContainer(Transform under) =>
      _instantiator.InstantiatePrefab(_provider.ParticularMonth(), under.transform);

    public void CreateHolidayDataAssembly(Transform under)
    {
      GameObject objectAssembly = _instantiator.InstantiatePrefab(_provider.HolidayDataAssembly(), under.transform);

      GameObject infoContainer = objectAssembly.GetComponent<HolidayAssembler>().InfoContainer();

      HeaderConfiguration(under: infoContainer);

      IconConfiguration(under: infoContainer);

      HolidayNameConfiguration(under: infoContainer);

      SuggestionsConfiguration(under: infoContainer);

      ContentTextConfiguration(under: infoContainer);

      DayIconsConfiguration(under: infoContainer);
    }

    private void HeaderConfiguration(GameObject under)
    {
      GameObject header;

      if (_clearData.IsWeekNameEmpty)
      {
        header = Instantiate(_provider.HeaderNoName(), under);
        header.GetComponent<HolidayHeader>().SetBackground(_clearData.HeaderColor);
        header.GetComponent<HolidayHeader>().SetDateMonth(_clearData.DateMonth);
        header.GetComponent<HolidayHeader>().SetWeekdayName(_clearData.WeekName);
      }
      else
      {
        header = Instantiate(_provider.HeaderWithName(), under);
        header.GetComponent<HolidayHeaderPlusWeekName>().SetBackground(_clearData.HeaderColor);
        header.GetComponent<HolidayHeaderPlusWeekName>().SetWeekdayName(_clearData.WeekdayName);
        header.GetComponent<HolidayHeaderPlusWeekName>().SetDateMonth(_clearData.DateMonth);
        header.GetComponent<HolidayHeaderPlusWeekName>().SetWeekName(_clearData.WeekName);
      }
    }

    private void IconConfiguration(GameObject under)
    {
      if(_clearData.IsShortView)
        return;
      
      GameObject icon = Instantiate(_provider.IconImage(), under);
      icon.GetComponent<IconSetup>().SetIcon(_clearData.MainIcon);
    }

    private void HolidayNameConfiguration(GameObject under)
    {
      if(_clearData.IsHolidayNameEmpty)
        return;
      
      GameObject holidayName = Instantiate(_provider.HolidayName(), under);
      holidayName.GetComponent<HolidayName>().SetHolidayName(_clearData.HolidayName);
    }

    private void SuggestionsConfiguration(GameObject under)
    {
      GameObject suggestion = Instantiate(_provider.Suggestion(), under);
      
      List<Sprite> suggestions = _clearData.Suggestions;
      
      for (int i = 0; i < suggestions.Count; i++) 
        Instantiate(_provider.SuggestionItem(), suggestion);
      
      suggestion.GetComponent<FillChildrenImages>().SetImagesWith(sprites: suggestions);
    }

    private void ContentTextConfiguration(GameObject under)
    {
      GameObject generalContentText = Instantiate(_provider.GeneralContentText(), under: under);
      
      generalContentText.GetComponent<ContentWriter>().SetContent(_clearData.ShortContentText);
    }

    private void DayIconsConfiguration(GameObject under)
    {
      if(!_clearData.IsAnyDayIcons)
        return;
      
      GameObject dayIconsContainer = Instantiate(_provider.DayIconsContainer(), under);

      List<Sprite> dayIcons = _clearData.DayIcons;
      
      for (int i = 0; i < dayIcons.Count; i++) 
        Instantiate(_provider.IconImage(), under);
      
      dayIconsContainer.GetComponent<FillChildrenImages>().SetImagesWith(sprites: dayIcons);
    }

    private GameObject Instantiate(GameObject prefab, GameObject under) =>
      _instantiator.InstantiatePrefab(prefab, under.transform);
  }
}