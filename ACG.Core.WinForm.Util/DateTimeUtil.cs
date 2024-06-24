using System;

namespace ACG.Core.WinForm.Util
{
    public class DateTimeUtil
    {
        public static DateTime DateStartOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public static DateTime DateEndOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999);
        }

        public static DateTime StartOfDate(DateTime date)
        {
            return date.Date;
        }

        public static DateTime EndOfDate(DateTime date)
        {
            return date.Date.AddDays(1).AddTicks(-1);
        }

        public static bool IsDateBetweenRange(DateTime fromDate, DateTime toDate, DateTime dateToCompare)
        {
            return dateToCompare >= fromDate && dateToCompare <= toDate;
        }

        public static bool IsSameMonthYear(DateTime date, DateTime dateToCompare)
        {
            return date.Year == dateToCompare.Year && date.Month == dateToCompare.Month;
        }

        public static bool Compare(DateTime date, DateTime dateToCompare, string compareFormat)
        {
            string dateStr = date.ToString(compareFormat);
            string dateToCompareStr = dateToCompare.ToString(compareFormat);
            return dateStr.Equals(dateToCompareStr);
        }

        public static int GetNumberDayOfWeekInMonth(int year, int month, DayOfWeek dayOfWeek)
        {
            int count = 0;
            int day = DateTime.DaysInMonth(year, month);

            for (; day > 0; day--)
            {
                DateTime d = new DateTime(year, month, day);
                //Compare date with sunday
                if (d.DayOfWeek == dayOfWeek)
                {
                    count = count + 1;
                }
            }

            return count;
        }
    }
}
