﻿using System;

namespace CodeBase.Extensions
{
  public static class StringExtensions
  {
    private const string JsonExtension = ".json";
    private const string Format = "yyyy-MM-dd";

    public static string Json(this string todayDate) => 
      todayDate + JsonExtension;

    public static string ToStringDateFormat(this DateTime date) => 
      date.ToString(Format);

    public static string WithoutYear(this string date) => 
      date.Substring(5);

    public static int ToInteger(this string year) => 
      int.Parse(year);

    public static string WithIndex(this string date, int index) => 
      date + $" ({index})";
  }
}