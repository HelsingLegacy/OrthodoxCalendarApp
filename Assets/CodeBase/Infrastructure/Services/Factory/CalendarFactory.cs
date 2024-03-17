using System.Collections.Generic;
using CodeBase.Data.Services;
using CodeBase.Data.Services.AssetProviding;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.UI.ContentHandlers.Interacting;
using CodeBase.UI.ContentHandlers.NonInteracting;
using CodeBase.UI.ContentHandlers.NonInteracting.HolidayComponents;
using CodeBase.UI.ContentHandlers.NonInteracting.HolidayComponents.Header;
using CodeBase.UI.Mediator;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.Factory
{
  public class CalendarFactory
  {
    private readonly DiContainer _container;
    private readonly IAssetProvider _provider;
    private readonly IConfigProvider _configProvider;

    public CalendarFactory(DiContainer container, IAssetProvider provider, IConfigProvider configProvider)
    {
      _container = container;
      _provider = provider;
      _configProvider = configProvider;
    }

    public GameObject CreateHudWithBinding()
    {
      UnbindDestroyedMainWindow();
      
      MainWindow hud = _container.InstantiatePrefabForComponent<MainWindow>(_provider.HudPrefab());

      _container.Bind<MainWindow>().FromInstance(hud);

      return hud.gameObject;
    }

    public void CreateMonthList(GameObject parent) =>
      Instantiate(_provider.MonthList(), parent);

    public void CreateShortInfo(GameObject under, string onDate)
    {
      GameObject objectAssembly = Instantiate(_provider.ShortHolidayAssembly(), under);

      // Return game object and build all in builder? 
    }

    public void CreateHolidayShortInfo(GameObject under, string onDate)
    {
      GameObject objectAssembly = Instantiate(_provider.ShortHolidayAssembly(), under);

      ShortHolidayAssembly assembly = objectAssembly.GetComponent<ShortHolidayAssembly>();

      GameObject container = assembly.InfoContainer();

      SetBackgroundColor(assembly, _configProvider.GetConfigFor(onDate));

      HeaderConfiguration(under: container, _configProvider.GetConfigFor(onDate));

      HolidayNameConfiguration(under: container, _configProvider.GetConfigFor(onDate));

      SuggestionsConfiguration(under: container, _configProvider.GetConfigFor(onDate));

      ShortContextTextConfiguration(under: container, _configProvider.GetConfigFor(onDate));

      assembly.SetDate(onDate);
    }

    public void CreateHolidayFullInfo(GameObject under, string onDate)
    {
      GameObject content = Instantiate(_provider.FullHolidayAssembly(), under);
      GameObject readings = Instantiate(_provider.HolidayReadings(), under);

      FullHolidayAssembler contentAssembler = content.GetComponent<FullHolidayAssembler>();
      ReadingAssembler readingAssembler = readings.GetComponent<ReadingAssembler>();

      GameObject container = contentAssembler.InfoContainer();

      SetBackgroundColor(contentAssembler, _configProvider.GetConfigFor(onDate));
      SetBackgroundColor(contentAssembler, readingAssembler, _configProvider.GetConfigFor(onDate));

      HeaderConfiguration(under: container, _configProvider.GetConfigFor(onDate));

      IconConfiguration(under: container, _configProvider.GetConfigFor(onDate));

      HolidayNameConfiguration(under: container, _configProvider.GetConfigFor(onDate));

      SuggestionsConfiguration(under: container, _configProvider.GetConfigFor(onDate));

      ShortContextTextConfiguration(under: container, _configProvider.GetConfigFor(onDate));

      ReadingGroupTitleAndCodeConfiguration(under: container, _configProvider.GetConfigFor(onDate));

      LiturgyConfiguration(under: container, _configProvider.GetConfigFor(onDate));

      ReadingsConfiguration(under: readings, _configProvider.GetConfigFor(onDate));

      DayIconsConfiguration(under: under.gameObject, _configProvider.GetConfigFor(onDate));
    }

    private void SetBackgroundColor(IHolidayAssembler content, ConfigAssembly configAssembly) =>
      content.SetBackgroundColor(configAssembly.TextBackgroundColor);

    private void SetBackgroundColor(IHolidayAssembler content, ReadingAssembler reading, ConfigAssembly configAssembly)
    {
      content.SetBackgroundColor(configAssembly.TextBackgroundColor);
      reading.SetBackgroundColor(configAssembly.TextBackgroundColor);
    }

    private void HeaderConfiguration(GameObject under, ConfigAssembly extracted)
    {
      GameObject header;

      if (EmptyWeekName())
      {
        header = Instantiate(_provider.HeaderNoName(), under);
        header.GetComponent<HolidayHeader>().SetBackground(extracted.HeaderColor);
        header.GetComponent<HolidayHeader>().SetWeekdayName(extracted.WeekdayName);
        header.GetComponent<HolidayHeader>().SetDateMonth(extracted.Day + extracted.Month);
      }
      else
      {
        header = Instantiate(_provider.HeaderWithName(), under);
        header.GetComponent<HolidayHeaderPlusWeekName>().SetBackground(extracted.HeaderColor);
        header.GetComponent<HolidayHeaderPlusWeekName>().SetWeekdayName(extracted.WeekdayName);
        header.GetComponent<HolidayHeaderPlusWeekName>().SetDateMonth(extracted.Day + extracted.Month);
        header.GetComponent<HolidayHeaderPlusWeekName>().SetWeekName(extracted.WeekName);
      }

      bool EmptyWeekName() =>
        string.IsNullOrEmpty(extracted.WeekName);
    }

    private void IconConfiguration(GameObject under, ConfigAssembly configAssembly)
    {
      // if (configAssembly.IsMobilePreview)
      //   return;

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
      GameObject generalContentText = Instantiate(_provider.GeneralContentText(), under: under);

      generalContentText.GetComponent<ContentWriter>().SetContent(configAssembly.ReadingsShortLinks);
    }

    private void LiturgyConfiguration(GameObject under, ConfigAssembly configAssembly)
    {
      GameObject generalContentText = Instantiate(_provider.GeneralContentText(), under: under);

      generalContentText.GetComponent<ContentWriter>().SetContent(configAssembly.LiturgyText);
    }

    private void ReadingsConfiguration(GameObject under, ConfigAssembly configAssembly)
    {
      under.GetComponent<ReadingAssembler>().SetReadings(configAssembly.EvangelionReadingsText);
    }

    private void DayIconsConfiguration(GameObject under, ConfigAssembly configAssembly)
    {
      if (configAssembly.DayIcons is { Count: > 0 })
      {
        GameObject dayIconsContainer = Instantiate(_provider.DayIconsContainer(), under);

        List<Sprite> dayIcons = configAssembly.DayIcons;

        for (int i = 0; i < dayIcons.Count; i++)
          Instantiate(_provider.IconImage(), dayIconsContainer);

        dayIconsContainer.GetComponent<FillChildrenImages>().SetImagesWith(sprites: dayIcons);
      }
    }

    private GameObject Instantiate(GameObject prefab, GameObject under) =>
      _container.InstantiatePrefab(prefab, under.transform);

    private GameObject Instantiate(GameObject prefab) =>
      _container.InstantiatePrefab(prefab);

    private void UnbindDestroyedMainWindow()
    {
      if (_container.HasBinding<MainWindow>())
        _container.Unbind<MainWindow>();
    }
  }
}