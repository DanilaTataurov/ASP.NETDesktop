using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NETDesktop.Common.Helpers {
    public static class DateHelper {
        public static int GetWorkingDays(DateTime startDate, DateTime endDate) {
            var totalDays = 0;
            var holidays = GetHolidays();
            for (var date = startDate; date <= endDate; date = date.AddDays(1)) {
                if (date.DayOfWeek != DayOfWeek.Saturday
                    && date.DayOfWeek != DayOfWeek.Sunday
                    && !holidays.Contains(date))
                    totalDays++;
            }
            return totalDays;
        }

        public static bool DatesIntersect(DateTime newStartDate, DateTime newEndDate, DateTime oldStartDate, DateTime oldEndDate) {
            if (newStartDate == newEndDate) {
                if (newStartDate == oldStartDate && newEndDate == oldStartDate)
                    return true;
                if (newEndDate == oldEndDate && newStartDate == oldEndDate)
                    return true;
            }

            if (newStartDate == oldStartDate || newEndDate == oldEndDate)
                return true;

            if (newStartDate >= oldStartDate && newEndDate <= oldEndDate) {
                return true;
            }

            if (newStartDate <= oldStartDate) {
                if (newEndDate >= oldStartDate && newEndDate <= oldEndDate)
                    return true;

                if (newEndDate >= oldEndDate)
                    return true;
            } else {
                if (oldEndDate >= newStartDate && oldEndDate <= newEndDate)
                    return true;

                if (oldEndDate >= newEndDate)
                    return true;
            }
            return false;
        }

        public static IEnumerable<DateTime> GetHolidays() {
            var Holidays = new List<DateTime>();
            Holidays.Add(new DateTime(DateTime.Now.Year, 1, 1));
            Holidays.Add(new DateTime(DateTime.Now.Year, 1, 2));
            Holidays.Add(new DateTime(DateTime.Now.Year, 1, 3));
            Holidays.Add(new DateTime(DateTime.Now.Year, 1, 4));
            Holidays.Add(new DateTime(DateTime.Now.Year, 1, 5));
            Holidays.Add(new DateTime(DateTime.Now.Year, 1, 6));
            Holidays.Add(new DateTime(DateTime.Now.Year, 1, 7));

            Holidays.Add(new DateTime(DateTime.Now.Year, 2, 24));
            Holidays.Add(new DateTime(DateTime.Now.Year, 3, 9));

            Holidays.Add(new DateTime(DateTime.Now.Year, 5, 1));
            Holidays.Add(new DateTime(DateTime.Now.Year, 5, 2));
            Holidays.Add(new DateTime(DateTime.Now.Year, 5, 3));
            Holidays.Add(new DateTime(DateTime.Now.Year, 5, 4));
            Holidays.Add(new DateTime(DateTime.Now.Year, 5, 8));
            Holidays.Add(new DateTime(DateTime.Now.Year, 5, 9));
            Holidays.Add(new DateTime(DateTime.Now.Year, 5, 10));

            Holidays.Add(new DateTime(DateTime.Now.Year, 6, 11));
            Holidays.Add(new DateTime(DateTime.Now.Year, 11, 4));

            Holidays.Add(new DateTime(DateTime.Now.Year, 12, 25));
            Holidays.Add(new DateTime(DateTime.Now.Year, 12, 26));
            Holidays.Add(new DateTime(DateTime.Now.Year, 12, 27));
            Holidays.Add(new DateTime(DateTime.Now.Year, 12, 28));
            Holidays.Add(new DateTime(DateTime.Now.Year, 12, 29));
            Holidays.Add(new DateTime(DateTime.Now.Year, 12, 30));
            Holidays.Add(new DateTime(DateTime.Now.Year, 12, 31));

            return Holidays;
        }
    }
}
