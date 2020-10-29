using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;


public static class Utilty
{
    public static string GetErrors(this ModelStateDictionary modelState)
    {
        return string.Join("<br />", (from item in modelState
                                      where item.Value.Errors.Any()
                                      select item.Value.Errors[0].ErrorMessage).ToList());
    }
    public static string ToLowerFirst(this string str)
    {
        return str.Substring(0, 1).ToLower() + str.Substring(1);
    }

    public static DateTime ToPersianDate(this DateTime dt)
    {
        PersianCalendar pc = new PersianCalendar();
        int year = pc.GetYear(dt);
        int month = pc.GetMonth(dt);
        int day = pc.GetDayOfMonth(dt);
        int hour = pc.GetHour(dt);
        int min = pc.GetMinute(dt);

        return new DateTime(year, month, day, hour, min, 0);
    }

    public static DateTime ToMiladiDate(this DateTime dt)
    {
        PersianCalendar pc = new PersianCalendar();
        return pc.ToDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0);
    }
}
