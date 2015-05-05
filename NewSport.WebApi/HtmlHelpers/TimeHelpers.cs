using System;
using System.Web.Mvc;

namespace NewSport.WebApi.HtmlHelpers
{
    public static class TimeHelpers
    {
        public static string HowMuchTimeHasPassedSinceSbAddedComment(this HtmlHelper helpers, DateTime date)
        {
            DateTime dateNow = DateTime.Now;
            TimeSpan timeSpan = dateNow.Subtract(date);
            int seconds = (int) timeSpan.TotalSeconds;
            if (seconds < 60)
            {
                return seconds + " sekund temu";
            }
            if (seconds >= 60 && seconds < 3600)
            {
                return (int)timeSpan.TotalMinutes + " minut temu";
            }
            if (seconds >= 3600 && seconds < 86400)
            {
                return (int)(timeSpan.TotalHours) + " godzin temu";
            }
            return (int)timeSpan.TotalDays + " dni temu";
        }
    }
}
