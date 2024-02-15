﻿using System.Collections.Generic;
using CodeBase.Data.Services.AssetProviding;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.TimeDate;
using CodeBase.UI.ContentFillers.NonInteracting;
using CodeBase.UI.ContentFillers.NonInteracting.HolidayComponents;
using CodeBase.UI.ContentFillers.NonInteracting.HolidayComponents.Header;
using CodeBase.UI.Mediator;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services
{
  public class CalendarFactory
  {
    private readonly DiContainer _container;
    private readonly IAssetProvider _provider;
    private readonly IHolidaysStorage _storage;
    private IToday _today;

    public CalendarFactory(DiContainer container, IAssetProvider provider, IHolidaysStorage storage)
    {
      _container = container;
      _provider = provider;
      _storage = storage;
    }

    public void CreateHudWithBinding()
    {
      GameObject hud = _container.InstantiatePrefab(_provider.HudPrefab());
      
      HudMediator mediator = hud.GetComponent<HudMediator>();
      
      hud.GetComponent<Shifting>().ShiftMediatorParent();
      
      mediator.ShowTodayHoliday();

      _container.Bind<HudMediator>().FromInstance(mediator);
    }

    public GameObject CreateContentContainer(GameObject under) => 
      Instantiate(_provider.ContentContainer(), under);

    public void CreateMonthList(HudMediator parent) => 
      Instantiate(_provider.MonthList(), parent.ContentContainer);

    public void CreateHolidayShortInfo(GameObject under, string onDate)
    {
      ConfigAssembly configAssembly = new ConfigAssembly(_storage, onDate);

      GameObject objectAssembly = Instantiate(_provider.HolidayDataAssembly(), under);
      
      HolidayAssembler assembler = objectAssembly.GetComponent<HolidayAssembler>();
      
      GameObject container = assembler.InfoContainer();

      SetBackgroundColor(assembler, configAssembly);
      
      HeaderConfiguration(under: container, configAssembly);

      HolidayNameConfiguration(under: container, configAssembly);

      SuggestionsConfiguration(under: container, configAssembly);

      ShortContextTextConfiguration(under: container, configAssembly);
    }

    public void CreateHolidayFullInfo(GameObject under, string onDate)
    {
      ConfigAssembly configAssembly = new ConfigAssembly(_storage, onDate);

      GameObject content = Instantiate(_provider.HolidayDataAssembly(), under);
      GameObject readings = Instantiate(_provider.HolidayReadings(), under);

      HolidayAssembler contentAssembler = content.GetComponent<HolidayAssembler>();
      ReadingAssembler readingAssembler = readings.GetComponent<ReadingAssembler>();
      
      GameObject container = contentAssembler.InfoContainer();
      
      SetBackgroundColor(contentAssembler, configAssembly);
      SetBackgroundColor(contentAssembler, readingAssembler, configAssembly);
      
      HeaderConfiguration(under: container, configAssembly);

      IconConfiguration(under: container, configAssembly);

      HolidayNameConfiguration(under: container, configAssembly);

      SuggestionsConfiguration(under: container, configAssembly);

      ShortContextTextConfiguration(under: container, configAssembly);

      ReadingGroupTitleAndCodeConfiguration(under: container, configAssembly);

      LiturgyConfiguration(under: container, configAssembly);

      ReadingsConfiguration(under: readings, configAssembly);
      
      DayIconsConfiguration(under: under.gameObject, configAssembly);
    }

    private void SetBackgroundColor(HolidayAssembler content, ConfigAssembly configAssembly) => 
      content.SetBackgroundColor(configAssembly.TextBackgroundColor);

    private void SetBackgroundColor(HolidayAssembler content, ReadingAssembler reading, ConfigAssembly configAssembly)
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
      if (configAssembly.DayIcons is {Count: > 0})
      {
        GameObject dayIconsContainer = Instantiate(_provider.DayIconsContainer(), under);

        List<Sprite> dayIcons = configAssembly.DayIcons;

        for (int i = 0; i < dayIcons.Count; i++)
          Instantiate(_provider.IconImage(), dayIconsContainer);

        dayIconsContainer.GetComponent<FillChildrenImages>().SetImagesWith(sprites: dayIcons);
      }
    }

    public void CreateNoticePopup() => 
      Instantiate(_provider.NoticePopup());

    private GameObject Instantiate(GameObject prefab, GameObject under) =>
      _container.InstantiatePrefab(prefab, under.transform);
    
    private GameObject Instantiate(GameObject prefab) =>
      _container.InstantiatePrefab(prefab);
  }
}