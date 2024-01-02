// using System;
// using System.Collections.Generic;
// using System.Linq;
// using CodeBase.Data.Services.JsonHandle;
// using CodeBase.Infrastructure.Services.TimeDate;
// using CodeBase.ScriptableData;
// using UnityEngine;
//
// namespace CodeBase.Data
// {
//   public class ClearData
//   {
//     private const string MobileBuildingData = "ScriptableData/MobileHolidayShortDataInfo.asset";
//
//     public Color HeaderColor;
//     public Color BackgroundColor;
//     public string WeekdayName;
//     public string DateMonth;
//
//     public bool IsWeekNameEmpty;
//     public string WeekName;
//
//     public bool IsShortView = true;
//     public Sprite MainIcon;
//
//     public bool IsHolidayNameEmpty = true;
//     public string HolidayName;
//
//     public List<Sprite> Suggestions;
//
//     public string ShortContentText;
//
//     public bool IsAnyDayIcons;
//     public List<Sprite> DayIcons;
//
//
//     private readonly IHolidayDataExtractor _extractedData;
//     private IToday _today;
//
//     public ClearData(IHolidayDataExtractor extractedData)
//     {
//       _extractedData = extractedData;
//     }
//
//     public void CleanDataForToday()
//     {
//       SetHeaderColor();
//       SetBackgroundColor();
//       SetWeekdayName();
//       SetDateMonth();
//       SetWeekName();
//       SetMainIcon();
//       SetHolidayName();
//       SetSuggestions();
//       SetShortContent();
//       SetDayIcons();
//     }
//
//     private void SetHeaderColor()
//     {
//       if (_extractedData.ExtractedData().HolidayColor.ToLower() == "black")
//         HeaderColor = BuildingData().HeaderBlack;
//       HeaderColor = BuildingData().HeaderRed;
//     }
//     
//     private void SetBackgroundColor()
//     {
//       if (_extractedData.ExtractedData().HolidayColor.ToLower() == "black")
//         BackgroundColor = BuildingData().BackgroundBlack;
//       BackgroundColor = BuildingData().BackgroundRed;
//     }
//
//     private void SetWeekdayName()
//     {
//       DayOfWeek dayOfWeek = _today.TodayKyiv().DayOfWeek;
//
//       switch (dayOfWeek)
//       {
//         case DayOfWeek.Sunday:
//           WeekdayName = "Неділя";
//           break;
//         case DayOfWeek.Monday:
//           WeekdayName = "Понеділок";
//           break;
//         case DayOfWeek.Tuesday:
//           WeekdayName = "Вівторок";
//           break;
//         case DayOfWeek.Wednesday:
//           WeekdayName = "Середа";
//           break;
//         case DayOfWeek.Thursday:
//           WeekdayName = "Четвер";
//           break;
//         case DayOfWeek.Friday:
//           WeekdayName = "П'ятниця";
//           break;
//         case DayOfWeek.Saturday:
//           WeekdayName = "Субота";
//           break;
//       }
//     }
//
//     private void SetDateMonth()
//     {
//       int day = _today.TodayKyiv().Day;
//       int month = _today.TodayKyiv().Month;
//
//       string monthName = "";
//
//       switch (month)
//       {
//         case 1:
//           monthName = " Січня";
//           break;
//         case 2:
//           monthName = " Лютого";
//           break;
//         case 3:
//           monthName = " Березня";
//           break;
//         case 4:
//           monthName = " Квітня";
//           break;
//         case 5:
//           monthName = " Травня";
//           break;
//         case 6:
//           monthName = " Червня";
//           break;
//         case 7:
//           monthName = " Липня";
//           break;
//         case 8:
//           monthName = " Серпня";
//           break;
//         case 9:
//           monthName = " Вересня";
//           break;
//         case 10:
//           monthName = " Жовтня";
//           break;
//         case 11:
//           monthName = " Листопада";
//           break;
//         case 12:
//           monthName = " Грудня";
//           break;
//       } 
//
//       DateMonth = day + monthName;
//     }
//
//     private void SetWeekName()
//     {
//       WeekName = _extractedData.ExtractedData().WeekName;
//
//       IsWeekNameEmpty = string.IsNullOrEmpty(WeekName);
//     }
//
//     private void SetMainIcon()
//     {
//       
//     }
//
//     private void SetHolidayName()
//     {
//       
//     }
//
//     private void SetSuggestions()
//     {
//       Suggestions.Add(BuildingData().DressRed);
//     }
//
//     private void SetShortContent()
//     {
//       string content = _extractedData.ExtractedData().Content;
//       string liturgy = _extractedData.ExtractedData().LiturgyRecommendations;
//
//       if (string.IsNullOrEmpty(liturgy))
//         ShortContentText = content;
//       else
//         ShortContentText = content + "\n" + liturgy;
//     }
//
//
//     private void SetDayIcons() => 
//       IsAnyDayIcons = DayIcons.Any();
//
//     private HolidaysBuildingData BuildingData() =>
//       Resources.Load<HolidaysBuildingData>(MobileBuildingData);
//   }
// }