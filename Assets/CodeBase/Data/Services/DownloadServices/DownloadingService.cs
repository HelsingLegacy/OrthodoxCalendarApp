﻿using System;
using System.IO;
using CodeBase.Data.Services.AssetProviding;
using CodeBase.Extensions;
using CodeBase.Infrastructure.Services.TimeDate;
using UnityEngine;

namespace CodeBase.Data.Services.DownloadServices
{
  public class DownloadingService : IDownloadingService
  {
    private readonly IDataLoaderService _dataLoader;
    private readonly IKyivDate _kyivDate;
    private readonly IHolidaysStorage _holidaysStorage;

    public DownloadingService(IDataLoaderService dataLoader, IKyivDate kyivDate, IHolidaysStorage holidaysStorage)
    {
      _dataLoader = dataLoader;
      _kyivDate = kyivDate;
      _holidaysStorage = holidaysStorage;
    }

    public async void LoadHoliday(Action onLoaded)
    {
      string date = _kyivDate.TodayKyiv().ToStringDateFormat();
      int progress = 0;

      if (!RequestedFileExist(date))
      {
        progress += await _dataLoader.LoadJson(date);
      }
      
      if (progress > 0)
        onLoaded?.Invoke();
      else
      {
        Debug.LogError($"Cannot download config for {date}.");
      }
    }
 
    public async void LoadHolidays(Action onLoaded)
    {
      DateTime startDate = _kyivDate.StartDate();
      DateTime endDate = _kyivDate.EndDate();

      TimeSpan datesBetween = endDate - startDate;

      int dates = datesBetween.Days + 1;
      int progress = 0;

      for (DateTime currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
      {
        string date = currentDate.ToStringDateFormat();

        if (!RequestedFileExist(date))
        {
          progress += await _dataLoader.LoadJson(date);
        }
        else
        {
          progress += 1;
        }
      }

      if (Mathf.Abs(dates - progress) < 0.01f)
      {
        onLoaded?.Invoke();
      }
    }

    private bool RequestedFileExist(string date) =>
      File.Exists(_holidaysStorage.HolidayFor(date));
  }
}