﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeBase.Data.DeserializationClasses;
using CodeBase.Extensions;
using CodeBase.ScriptableData;
using UnityEngine;

namespace CodeBase.Data.Services.AssetProviding
{
  public class ConfigAssembly
  {
    private const string MobileBuildingData = "ScriptableData/MobileHolidayShortDataInfo";

    public Color HeaderColor { get; private set; }
    public Color TextBackgroundColor { get; private set; }
    public string WeekdayName { get; private set; }
    public string Day { get; private set; }
    public string Month { get; private set; }

    public string WeekName { get; private set; }

    public bool IsMobilePreview => true; //{ get; private set; }
    public Sprite MainIcon { get; private set; }

    public string HolidayName { get; private set; }

    public List<Sprite> Suggestions { get; private set; }

    public string ShortContentText { get; private set; }

    public string ReadingsShortLinks { get; private set; }
    public string LiturgyText { get; private set; }

    public string EvangelionReadingsText { get; private set; }

    public List<Sprite> DayIcons { get; private set; }

    public ConfigAssembly(IHolidaysDataStorage dataStorage, string date)
    {
      ExtractedData(dataStorage, date);
    }

    private void ExtractedData(IHolidaysDataStorage dataStorage, string date)
    {
      string jsonText = File.ReadAllText(dataStorage.HolidayConfigFor(date));

      RawHolidayInfo info = jsonText.ToDeserialize<RawHolidayInfo>();

      SetCleanDataForToday(info, date, dataStorage);
    }

    private void SetCleanDataForToday(RawHolidayInfo info, string date, IHolidaysDataStorage dataStorage)
    {
      SetHolidayColor(info);
      SetWeekdayName(info);
      SetDateMonth(info);
      SetWeekName(info);
      SetMainIcon(dataStorage, date);
      SetHolidayName(info);
      SetSuggestions(info);
      SetShortContent(info);
      SetLiturgy(info);
      SetReadingsShortLinks(info);
      SetReadingsContent(info);
      SetDayIcons(info, dataStorage, date);
    }

    private void SetHolidayColor(RawHolidayInfo info)
    {
      string color = info.HolidayColor;

      if (color == "black")
      {
        HeaderColor = BuildingData().HeaderBlack;
        TextBackgroundColor = BuildingData().BackgroundBlack;
      }
      else
      {
        HeaderColor = BuildingData().HeaderRed;
        TextBackgroundColor = BuildingData().BackgroundRed;
      }
    }

    private void SetWeekdayName(RawHolidayInfo info)
    {
      DayOfWeek dayOfWeek = info.HolidayDate.DayOfWeek;

      switch (dayOfWeek)
      {
        case DayOfWeek.Sunday:
          WeekdayName = "Неділя";
          break;
        case DayOfWeek.Monday:
          WeekdayName = "Понеділок";
          break;
        case DayOfWeek.Tuesday:
          WeekdayName = "Вівторок";
          break;
        case DayOfWeek.Wednesday:
          WeekdayName = "Середа";
          break;
        case DayOfWeek.Thursday:
          WeekdayName = "Четвер";
          break;
        case DayOfWeek.Friday:
          WeekdayName = "П'ятниця";
          break;
        case DayOfWeek.Saturday:
          WeekdayName = "Субота";
          break;
      }
    }

    private void SetDateMonth(RawHolidayInfo info)
    {
      int day = info.HolidayDate.Day;
      int month = info.HolidayDate.Month;

      string monthName = "";

      switch (month)
      {
        case 1:
          monthName = " Січня";
          break;
        case 2:
          monthName = " Лютого";
          break;
        case 3:
          monthName = " Березня";
          break;
        case 4:
          monthName = " Квітня";
          break;
        case 5:
          monthName = " Травня";
          break;
        case 6:
          monthName = " Червня";
          break;
        case 7:
          monthName = " Липня";
          break;
        case 8:
          monthName = " Серпня";
          break;
        case 9:
          monthName = " Вересня";
          break;
        case 10:
          monthName = " Жовтня";
          break;
        case 11:
          monthName = " Листопада";
          break;
        case 12:
          monthName = " Грудня";
          break;
      }

      Day = $"{day}";
      Month = monthName;
    }

    private void SetWeekName(RawHolidayInfo info) =>
      WeekName = info.WeekName;

    private void SetMainIcon(IHolidaysDataStorage dataStorage, string date) =>
      MainIcon = SpriteProvider(dataStorage, date);

    private void SetHolidayName(RawHolidayInfo info) =>
      HolidayName = info.HolidayName;

    private void SetSuggestions(RawHolidayInfo info)
    {
      Suggestions = new List<Sprite>();

      SetHolidayIfExist(info);
      SetFest();
      SetSpecial(info);
      SetDress(info);

      void SetHolidayIfExist(RawHolidayInfo rawInfo)
      {
        if (rawInfo.HolidayCategory.Count > 0)
        {
          foreach (HolidayCategory holidayCategory in rawInfo.HolidayCategory)
          {
            switch (holidayCategory.Slug)
            {
              case "bdinnya":
                Suggestions.Add(BuildingData().CategoryVigil);
                break;
              case "velike-svyato":
                Suggestions.Add(BuildingData().CategoryHoliday);
                break;
              case "dvunadesyate":
                Suggestions.Add(BuildingData().CategoryTwelfth);
                break;
              case "pasha":
                Suggestions.Add(BuildingData().CategoryEaster);
                break;
              case "poliyelej":
                Suggestions.Add(BuildingData().CategoryHymn);
                break;
              case "slavosloviye":
                Suggestions.Add(BuildingData().CategoryGlorification);
                break;
              case "shesterichna":
                Suggestions.Add(BuildingData().CategoryHexoChordal);
                break;
            }
          }
        }
      }

      void SetFest()
      {
        switch (info.HolidayFast.Slug.ToLower())
        {
          case "chicken":
            Suggestions.Add(BuildingData().FastUnlimited);
            break;
          case "fish":
            Suggestions.Add(BuildingData().FastFish);
            break;
          case "olive-oil":
            Suggestions.Add(BuildingData().FastOil);
            break;
          case "vegetables":
            Suggestions.Add(BuildingData().FastStrict);
            break;
          case "no-food":
            Suggestions.Add(BuildingData().FastAbstinence);
            break;
        }
      }

      void SetSpecial(RawHolidayInfo rawInfo)
      {
        if (rawInfo.HolidaySpecial != null)
        {
          foreach (HolidaySpecial special in rawInfo.HolidaySpecial)
          {
            switch (special.Slug.ToLower())
            {
              case "funeral":
                Suggestions.Add(BuildingData().Funeral);
                break;
            }
          }
        }
      }

      void SetDress(RawHolidayInfo rawInfo)
      {
        if (rawInfo.HolidayDress is { Count: > 0 })
          foreach (HolidayDress holidayDress in rawInfo.HolidayDress)
          {
            switch (holidayDress.Slug)
            {
              case "burgundy":
                Suggestions.Add(BuildingData().DressBurgundy);
                break;
              case "violet":
                Suggestions.Add(BuildingData().DressViolet);
                break;
              case "white":
                Suggestions.Add(BuildingData().DressWhite);
                break;
              case "green":
                Suggestions.Add(BuildingData().DressGreen);
                break;
              case "blue":
                Suggestions.Add(BuildingData().DressBlue);
                break;
              case "red":
                Suggestions.Add(BuildingData().DressRed);
                break;
              case "yellow":
                Suggestions.Add(BuildingData().DressYellow);
                break;
            }
          }
      }
    }

    private void SetShortContent(RawHolidayInfo info) =>
      ShortContentText = info.Content;

    private void SetReadingsShortLinks(RawHolidayInfo info)
    {
      var groupedTitles = info.ReadingGroupList
        .GroupBy(mc => mc.Title)
        .Select(group => $"<b>{group.Key}</b>\n<u>{string.Join("\n", group.Select(mc => mc.Code))}</u>");

      string result = string.Join("\n", groupedTitles);

      ReadingsShortLinks = result;
    }

    private void SetLiturgy(RawHolidayInfo info) =>
      LiturgyText = info.LiturgyRecommendations;

    private void SetReadingsContent(RawHolidayInfo info)
    {
      string readings = "";

      foreach (ReadingGroup reading in info.ReadingGroupList)
      {
        readings += $"<b>{reading.Title}</b>\n";
        readings += $"<u>{reading.Code}</u>\n";
        readings += $"{reading.Text} \n";
      }

      readings += $"<size=70%><i>{info.ReadingGroupList[0].Copyright}</i></size>";

      EvangelionReadingsText = readings;
    }

    private void SetDayIcons(RawHolidayInfo info, IHolidaysDataStorage dataStorage, string date)
    {
      if (info.DayIcons is { Count: > 0 })
      {
        DayIcons = new List<Sprite>();

        for (int i = 1; i <= info.DayIcons.Count; i++)
        {
          DayIcons.Add(SpriteProvider(dataStorage, date.WithIndex(i)));
        }
      }
    }

    private static Sprite SpriteProvider(IHolidaysDataStorage dataStorage, string date)
    {
      var icon = File.ReadAllBytes(dataStorage.HolidayIconFor(date));

      Texture2D texture = new Texture2D(340, 377);
      texture.LoadImage(icon);

      Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

      return sprite;
    }

    private HolidaysBuildingData BuildingData() =>
      Resources.Load<HolidaysBuildingData>(MobileBuildingData);
  }
}