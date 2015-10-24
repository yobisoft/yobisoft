using System;
using System.Linq;

namespace Yobisoft.Core.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime ChangeYears(this DateTime dateTime, int years)
        {
            return new DateTime(
                years,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second,
                dateTime.Millisecond,
                dateTime.Kind);
        }

        public static DateTime ChangeMonths(this DateTime dateTime, int months)
        {
            return new DateTime(
                dateTime.Year,
                months,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second,
                dateTime.Millisecond,
                dateTime.Kind);
        }

        public static DateTime ChangeDays(this DateTime dateTime, int days)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                days,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second,
                dateTime.Millisecond,
                dateTime.Kind);
        }

        public static DateTime ChangeHours(this DateTime dateTime, int hours)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                dateTime.Minute,
                dateTime.Second,
                dateTime.Millisecond,
                dateTime.Kind);
        }
        public static DateTime ChangeMinutes(this DateTime dateTime, int minutes)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                minutes,
                dateTime.Second,
                dateTime.Millisecond,
                dateTime.Kind);
        }

        public static DateTime ChangeSeconds(this DateTime dateTime, int seconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                seconds,
                dateTime.Millisecond,
                dateTime.Kind);
        }

        public static DateTime ChangeMilliseconds(this DateTime dateTime, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second,
                milliseconds,
                dateTime.Kind);
        }

        public static DateTime Max(params DateTime[] timestamps)
        {
            return timestamps.Max();
        }

        public static DateTime Min(params DateTime[] timestamps)
        {
            return timestamps.Min();
        }
    }
}
