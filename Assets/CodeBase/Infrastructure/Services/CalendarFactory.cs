using System.Collections.Generic;
using CodeBase.Data.Services;
using CodeBase.Data.Services.JsonHandle;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.TimeDate;
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
    private readonly IHolidaysStorage _storage;
    private IToday _today;

    public CalendarFactory(IInstantiator instantiator, IAssetProvider provider, IHolidaysStorage storage)
    {
      _instantiator = instantiator;
      _provider = provider;
      _storage = storage;
    }

    public GameObject CreateHud() =>
      _instantiator.InstantiatePrefab(_provider.HudPrefab());

    public GameObject CreateMonthContainer(Transform under) =>
      _instantiator.InstantiatePrefab(_provider.ParticularMonth(), under.transform);

    public void CreateHolidayDataAssembly(Transform under, string on)
    {
      HolidayDataExtractor extractor = new HolidayDataExtractor(_storage, on);

      GameObject objectAssembly = _instantiator.InstantiatePrefab(_provider.HolidayDataAssembly(), under.transform);

      GameObject container = objectAssembly.GetComponent<HolidayAssembler>().InfoContainer();
      

      HeaderConfiguration(under: container, extractor);

      IconConfiguration(under: container, extractor);

      HolidayNameConfiguration(under: container, extractor);

      SuggestionsConfiguration(under: container, extractor);

      ContentTextConfiguration(under: container, extractor);

      DayIconsConfiguration(under: container, extractor);
    }

    private void HeaderConfiguration(GameObject under, HolidayDataExtractor extracted)
    {
      GameObject header;

      if (EmptyWeekName())
      {
        header = Instantiate(_provider.HeaderNoName(), under);
        header.GetComponent<HolidayHeader>().SetBackground(extracted.HeaderColor);
        header.GetComponent<HolidayHeader>().SetWeekdayName(extracted.WeekdayName);
        header.GetComponent<HolidayHeader>().SetDateMonth(extracted.DateMonth);
      }
      else
      {
        header = Instantiate(_provider.HeaderWithName(), under);
        header.GetComponent<HolidayHeaderPlusWeekName>().SetBackground(extracted.HeaderColor);
        header.GetComponent<HolidayHeaderPlusWeekName>().SetWeekdayName(extracted.WeekdayName);
        header.GetComponent<HolidayHeaderPlusWeekName>().SetDateMonth(extracted.DateMonth);
        header.GetComponent<HolidayHeaderPlusWeekName>().SetWeekName(extracted.WeekName);
      }

      bool EmptyWeekName() =>
        string.IsNullOrEmpty(extracted.WeekName);
    }

    private void IconConfiguration(GameObject under, HolidayDataExtractor extractor)
    {
      if (extractor.IsMobilePreview) 
        return;
      
      GameObject icon = Instantiate(_provider.IconImage(), under);
      icon.GetComponent<IconSetup>().SetIcon(extractor.MainIcon);
    }

    private void HolidayNameConfiguration(GameObject under, HolidayDataExtractor extractor)
    {
      if (!string.IsNullOrEmpty(extractor.HolidayName))
      {
        GameObject holidayName = Instantiate(_provider.HolidayName(), under);
        holidayName.GetComponent<HolidayName>().SetHolidayName(extractor.HolidayName);
      }
    }

    private void SuggestionsConfiguration(GameObject under, HolidayDataExtractor extractor)
    {
      GameObject suggestion = Instantiate(_provider.Suggestion(), under);

      List<Sprite> suggestions = extractor.Suggestions;

      for (int i = 0; i < suggestions.Count; i++)
        Instantiate(_provider.SuggestionItem(), suggestion);

      suggestion.GetComponent<FillChildrenImages>().SetImagesWith(sprites: suggestions);
    }

    private void ContentTextConfiguration(GameObject under, HolidayDataExtractor extractor)
    {
      GameObject generalContentText = Instantiate(_provider.GeneralContentText(), under: under);

      generalContentText.GetComponent<ContentWriter>().SetContent(extractor.ShortContentText);
    }

    private void DayIconsConfiguration(GameObject under, HolidayDataExtractor extractor)
    {
      if (!extractor.IsAnyDayIcons)
        return;

      GameObject dayIconsContainer = Instantiate(_provider.DayIconsContainer(), under);

      List<Sprite> dayIcons = extractor.DayIcons;

      for (int i = 0; i < dayIcons.Count; i++)
        Instantiate(_provider.IconImage(), under);

      dayIconsContainer.GetComponent<FillChildrenImages>().SetImagesWith(sprites: dayIcons);
    }

    private GameObject Instantiate(GameObject prefab, GameObject under) =>
      _instantiator.InstantiatePrefab(prefab, under.transform);
  }
}