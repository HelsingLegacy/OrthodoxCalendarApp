using System.Collections.Generic;
using CodeBase.Data.Services;
using CodeBase.Data.Services.AssetProviding;
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
      ClearConfig clearConfig = new ClearConfig(_storage, on);

      GameObject objectAssembly = _instantiator.InstantiatePrefab(_provider.HolidayDataAssembly(), under.transform);

      GameObject container = objectAssembly.GetComponent<HolidayAssembler>().InfoContainer();
      

      HeaderConfiguration(under: container, clearConfig);

      IconConfiguration(under: container, clearConfig);

      HolidayNameConfiguration(under: container, clearConfig);

      SuggestionsConfiguration(under: container, clearConfig);

      ContentTextConfiguration(under: container, clearConfig);

      DayIconsConfiguration(under: container, clearConfig);
    }

    private void HeaderConfiguration(GameObject under, ClearConfig extracted)
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

    private void IconConfiguration(GameObject under, ClearConfig clearConfig)
    {
      if (clearConfig.IsMobilePreview) 
        return;
      
      GameObject icon = Instantiate(_provider.IconImage(), under);
      icon.GetComponent<IconSetup>().SetIcon(clearConfig.MainIcon);
    }

    private void HolidayNameConfiguration(GameObject under, ClearConfig clearConfig)
    {
      if (!string.IsNullOrEmpty(clearConfig.HolidayName))
      {
        GameObject holidayName = Instantiate(_provider.HolidayName(), under);
        holidayName.GetComponent<HolidayName>().SetHolidayName(clearConfig.HolidayName);
      }
    }

    private void SuggestionsConfiguration(GameObject under, ClearConfig clearConfig)
    {
      GameObject suggestion = Instantiate(_provider.Suggestion(), under);

      List<Sprite> suggestions = clearConfig.Suggestions;

      for (int i = 0; i < suggestions.Count; i++)
        Instantiate(_provider.SuggestionItem(), suggestion);

      suggestion.GetComponent<FillChildrenImages>().SetImagesWith(sprites: suggestions);
    }

    private void ContentTextConfiguration(GameObject under, ClearConfig clearConfig)
    {
      GameObject generalContentText = Instantiate(_provider.GeneralContentText(), under: under);

      generalContentText.GetComponent<ContentWriter>().SetContent(clearConfig.ShortContentText);
    }

    private void DayIconsConfiguration(GameObject under, ClearConfig clearConfig)
    {
      if (!clearConfig.IsAnyDayIcons)
        return;

      GameObject dayIconsContainer = Instantiate(_provider.DayIconsContainer(), under);

      List<Sprite> dayIcons = clearConfig.DayIcons;

      for (int i = 0; i < dayIcons.Count; i++)
        Instantiate(_provider.IconImage(), under);

      dayIconsContainer.GetComponent<FillChildrenImages>().SetImagesWith(sprites: dayIcons);
    }

    private GameObject Instantiate(GameObject prefab, GameObject under) =>
      _instantiator.InstantiatePrefab(prefab, under.transform);
  }
}