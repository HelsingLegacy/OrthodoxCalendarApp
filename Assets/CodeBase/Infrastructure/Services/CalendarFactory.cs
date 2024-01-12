using System.Collections.Generic;
using CodeBase.Data.Services.AssetProviding;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI.ContentFiller;
using CodeBase.UI.ContentFiller.HolidayComponents;
using CodeBase.UI.ContentFiller.HolidayComponents.Header;
using ModestTree;
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

    public void CreateHolidayShortInfo(Transform under, string on)
    {
      ConfigAssembly configAssembly = new ConfigAssembly(_storage, on);

      GameObject objectAssembly = _instantiator.InstantiatePrefab(_provider.HolidayDataAssembly(), under.transform);

      GameObject container = objectAssembly.GetComponent<HolidayAssembler>().InfoContainer();

      HeaderConfiguration(under: container, configAssembly);

      HolidayNameConfiguration(under: container, configAssembly);

      SuggestionsConfiguration(under: container, configAssembly);

      ShortContextTextConfiguration(under: container, configAssembly);
    }

    public void CreateHolidayFullInfo(Transform under, string on)
    {
      ConfigAssembly configAssembly = new ConfigAssembly(_storage, on);

      GameObject objectAssembly = _instantiator.InstantiatePrefab(_provider.HolidayDataAssembly(), under.transform);

      GameObject container = objectAssembly.GetComponent<HolidayAssembler>().InfoContainer();

      HeaderConfiguration(under: container, configAssembly);

      IconConfiguration(under: container, configAssembly);

      HolidayNameConfiguration(under: container, configAssembly);

      SuggestionsConfiguration(under: container, configAssembly);

      ShortContextTextConfiguration(under: container, configAssembly);

      ReadingGroupTitleAndCodeConfiguration(under: container, configAssembly);

      LiturgyConfiguration(under: container, configAssembly);

      EvangelionReadingsConfiguration(under: container, configAssembly);

      DayIconsConfiguration(under: container, configAssembly);
    }

    private void HeaderConfiguration(GameObject under, ConfigAssembly extracted)
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

    private void IconConfiguration(GameObject under, ConfigAssembly configAssembly)
    {
      if (configAssembly.IsMobilePreview)
        return;

      GameObject icon = Instantiate(_provider.IconImage(), under);
      icon.GetComponent<IconSetup>().SetIcon(configAssembly.MainIcon);
    }

    private void HolidayNameConfiguration(GameObject under, ConfigAssembly configAssembly)
    {
      if (!string.IsNullOrEmpty(configAssembly.HolidayName))
      {
        GameObject holidayName = Instantiate(_provider.HolidayName(), under);
        holidayName.GetComponent<HolidayName>().SetHolidayName(configAssembly.HolidayName);
      }
    }

    private void SuggestionsConfiguration(GameObject under, ConfigAssembly configAssembly)
    {
      GameObject suggestion = Instantiate(_provider.Suggestion(), under);

      List<Sprite> suggestions = configAssembly.Suggestions;

      for (int i = 0; i < suggestions.Count; i++)
        Instantiate(_provider.SuggestionItem(), suggestion);

      suggestion.GetComponent<FillChildrenImages>().SetImagesWith(sprites: suggestions);
    }

    private void ShortContextTextConfiguration(GameObject under, ConfigAssembly configAssembly)
    {
      GameObject generalContentText = Instantiate(_provider.GeneralContentText(), under: under);

      generalContentText.GetComponent<ContentWriter>().SetContent(configAssembly.ShortContentText);
    }

    private void ReadingGroupTitleAndCodeConfiguration(GameObject under, ConfigAssembly configAssembly)
    {
    }

    private void LiturgyConfiguration(GameObject under, ConfigAssembly configAssembly)
    {
    }

    private void EvangelionReadingsConfiguration(GameObject under, ConfigAssembly configAssembly)
    {
    }

    private void DayIconsConfiguration(GameObject under, ConfigAssembly configAssembly)
    {
      if (configAssembly.DayIcons.IsEmpty())
        return;

      GameObject dayIconsContainer = Instantiate(_provider.DayIconsContainer(), under);

      List<Sprite> dayIcons = configAssembly.DayIcons;

      for (int i = 0; i < dayIcons.Count; i++)
        Instantiate(_provider.IconImage(), under);

      dayIconsContainer.GetComponent<FillChildrenImages>().SetImagesWith(sprites: dayIcons);
    }

    private GameObject Instantiate(GameObject prefab, GameObject under) =>
      _instantiator.InstantiatePrefab(prefab, under.transform);
  }
}