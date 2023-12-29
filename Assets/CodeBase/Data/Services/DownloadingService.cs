﻿using System;
using System.IO;
using CodeBase.Data.Services.JsonHandle;
using CodeBase.Extensions;
using CodeBase.Infrastructure.Services.TimeDate;
using UnityEngine;

namespace CodeBase.Data.Services
{
  public class DownloadingService : IDownloadingService
  {
    private readonly IJsonSaver _jsonSaver;
    private readonly IKyivDate _kyivDate;
    private readonly IHolidaysStorage _holidaysStorage;

    public DownloadingService(IJsonSaver jsonSaver, IKyivDate kyivDate, IHolidaysStorage holidaysStorage)
    {
      _jsonSaver = jsonSaver;
      _kyivDate = kyivDate;
      _holidaysStorage = holidaysStorage;
    }

    public void LoadHolidays()
    {
      DateTime startDate = _kyivDate.StartDate();
      DateTime endDate = _kyivDate.EndDate();
      
      for (DateTime currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
      {
        string date = currentDate.ToStringDateFormat();
        
        if(!RequestedFileExist(date))
          _jsonSaver.LoadJsonFor(date);
        else
        {
          Debug.Log($"Holiday for {date} already exist");
        }
      }
    }
    
    private bool RequestedFileExist(string date) => 
      File.Exists(_holidaysStorage.HolidayFor(date));

  }
}