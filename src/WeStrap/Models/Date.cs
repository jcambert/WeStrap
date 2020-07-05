using System;
using System.Collections.Generic;
using System.Globalization;

namespace WeStrap
{
    public class Date
    {
        private readonly DateTime _start;
        private readonly CultureInfo _ci;
        private readonly Calendar _cal;
        private readonly CalendarWeekRule _weekRule;
        private readonly DayOfWeek _dayOfWeek;

        public Date() : this(null, null)
        {

        }
        public Date(int year, int month, string cultureInfo = "") : this(year, month, 1, cultureInfo)
        {

        }
        public Date(int year, int month, int day, string cultureInfo = "") : this(new DateTime(year, month, day), cultureInfo)
        {

        }
        public Date(Nullable<DateTime> start, string cultureInfo = "") : this(start ?? DateTime.Now, string.IsNullOrEmpty(cultureInfo) ? CultureInfo.CurrentCulture : new CultureInfo(cultureInfo))
        { }

        public Date(DateTime start, CultureInfo cultureInfo)
        {
            _start = start;
            _ci = cultureInfo ?? CultureInfo.CurrentCulture;
            _cal = _ci.Calendar;
            _weekRule = _ci.DateTimeFormat.CalendarWeekRule;
            _dayOfWeek = _ci.DateTimeFormat.FirstDayOfWeek;

            FirstDayOfMonth = new DateTime(Year, Month, 1).DayOfWeek;
            LastDayOfMonth = new DateTime(Year, Month, DayInMonth).DayOfWeek;
            var idx = FirstDayOfMonth - DayOfWeek;
            List<int[]> month = new List<int[]>();

            var day = 1;
            while (day <= DayInMonth)
            {
                int[] week = new int[7];
                for (int i = idx; i < 7; i++)
                {
                    week[i] = day++;
                    if (day > DayInMonth) break;
                }
                idx = 0;
                month.Add(week);
            }
            WeekMap = month;
        }


        public DayOfWeek FirstDayOfMonth { get; }
        public DayOfWeek LastDayOfMonth { get; }
        public int Day => _start.Day;
        public int Year => _start.Year;
        public int Month => _start.Month;
        public DayOfWeek DayOfWeek => _dayOfWeek;
        public int DayOfYear => _start.DayOfYear;
        public int DayInMonth => _cal.GetDaysInMonth(_start.Year, _start.Month);
        public int DayInYear => _cal.GetDaysInYear(_start.Year);
        public int MonthsInYear => _cal.GetMonthsInYear(_start.Year);
        public int WeekOfYear => _cal.GetWeekOfYear(_start, _weekRule, _dayOfWeek);
        public List<int[]> WeekMap { get; }
        public DateTime Value => _start;
        public CultureInfo Culture => _ci;
        public static Date operator +(Date date, int month) => new Date(date.Value.AddMonths(month), date.Culture);
        public static Date operator -(Date date, int month) => new Date(date.Value.AddMonths(month * -1), date.Culture);
        public static Date operator ++(Date date) => new Date(date.Value.AddMonths(1), date.Culture);
        public static Date operator --(Date date) => new Date(date.Value.AddMonths(-1), date.Culture);

        public static implicit operator DateTime(Date date) => date.Value;

        public static implicit operator Date(DateTime date) => new Date(date);
    }

    public static class DateExtensions
    {
        public static Date NextMonth(this Date date) => ++date;
        public static Date PreviousMonth(this Date date) => --date;
    }
}
