using System;
using System.Collections.Generic;
using System.IO;
using CodeBase.Extensions;
using CodeBase.ScriptableData;
using UnityEngine;

namespace CodeBase.Data.Services.JsonHandle
{
  public class HolidayDataExtractor
  {
    private const string MobileBuildingData = "ScriptableData/MobileHolidayShortDataInfo";

    public Color HeaderColor { get; private set; }
    public Color TextBackgroundColor { get; private set; }
    public string WeekdayName { get; private set; }
    public string DateMonth { get; private set; }

    public bool IsWeekNameEmpty { get; private set; }
    public string WeekName { get; private set; }

    public bool IsShortView => true;
    public Sprite MainIcon { get; private set; }

    public bool IsHolidayName { get; private set; }
    public string HolidayName { get; private set; }

    public List<Sprite> Suggestions { get; private set; }

    public string ShortContentText { get; private set; }

    public bool IsAnyDayIcons { get; private set; }
    public List<Sprite> DayIcons { get; private set; }

    public HolidayDataExtractor(IHolidaysStorage storage, string date)
    {
      ExtractedData(storage, date);
    }

    public void ExtractedData(IHolidaysStorage storage, string date)
    {
      string jsonFilePath = storage.HolidayFor(date);

      string jsonText = File.ReadAllText(jsonFilePath);

      RawHolidayInfo info = jsonText.ToDeserialize<RawHolidayInfo>();

      SetCleanDataForToday(info);
    }

    private void SetCleanDataForToday(RawHolidayInfo info)
    {
      SetHolidayColor(info);
      SetWeekdayName(info);
      SetDateMonth(info);
      SetWeekName(info);
      SetMainIcon(info);
      SetHolidayName(info);
      SetSuggestions(info);
      SetShortContent(info);
      SetDayIcons();
    }

    private void SetHolidayColor(RawHolidayInfo info)
    {
      if (info.HolidayColor.ToLower() == "black")
      {
        HeaderColor = BuildingData().HeaderBlack;
        TextBackgroundColor = BuildingData().BackgroundBlack;
      }

      HeaderColor = BuildingData().HeaderRed;
      TextBackgroundColor = BuildingData().BackgroundRed;
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
      DateTime.TryParse(info.Title, out DateTime date);

      int day = date.Day;
      int month = date.Month;

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

      DateMonth = day + monthName;
    }

    private void SetWeekName(RawHolidayInfo info)
    {
      WeekName = info.WeekName;

      IsWeekNameEmpty = string.IsNullOrEmpty(WeekName);
    }

    private void SetMainIcon(RawHolidayInfo info)
    {
    }

    private void SetHolidayName(RawHolidayInfo info)
    {
      if (string.IsNullOrEmpty(info.HolidayName.ToLower()))
        IsHolidayName = false;
      else
      {
        IsHolidayName = true;
        HolidayName = info.HolidayName;
      }
    }

    private void SetSuggestions(RawHolidayInfo info)
    {
      Suggestions = new List<Sprite>();

      SetHolidayIfExist(info);
      SetFest();
      SetSpecial(info);
      SetDress(info);


      void SetHolidayIfExist(RawHolidayInfo rawInfo)
      {
        if (rawInfo.HolidayCategoryList.Count > 0)
        {
          foreach (HolidayCategory holidayCategory in rawInfo.HolidayCategoryList)
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
          case "":
            Suggestions.Add(BuildingData().FestUnlimited);
            break;
          case "fish":
            Suggestions.Add(BuildingData().FestFish);
            break;
          case "oil":
            Suggestions.Add(BuildingData().FestOil);
            break;
          case "strict":
            Suggestions.Add(BuildingData().FestStrict);
            break;
          case "abstinence":
            Suggestions.Add(BuildingData().FestAbstinence);
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

    private void SetShortContent(RawHolidayInfo info)
    {
      string content = info.Content;
      string liturgy = info.LiturgyRecommendations;

      if (string.IsNullOrEmpty(liturgy))
        ShortContentText = content;
      else
        ShortContentText = content + "\n" + liturgy;
    }


    private void SetDayIcons() =>
      IsAnyDayIcons = false; //DayIcons.Any();

    private HolidaysBuildingData BuildingData() =>
      Resources.Load<HolidaysBuildingData>(MobileBuildingData);
  }
}