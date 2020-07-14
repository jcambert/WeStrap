using System;
using System.Collections.Generic;
using System.Globalization;

namespace WeCommon
{
    public class Date
    {
        private readonly Calendar _cal;
        private readonly CalendarWeekRule _weekRule;

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
            Value = start;
            Culture = cultureInfo ?? CultureInfo.CurrentCulture;
            _cal = Culture.Calendar;
            _weekRule = Culture.DateTimeFormat.CalendarWeekRule;
            DayOfWeek = Culture.DateTimeFormat.FirstDayOfWeek;

            FirstDayOfMonth = new DateTime(Year, Month, 1).DayOfWeek;
            LastDayOfMonth = new DateTime(Year, Month, DayInMonth).DayOfWeek;
            var idx = FirstDayOfMonth - DayOfWeek;
            if (idx < 0) idx = 6;
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
        public int Day => Value.Day;
        public int Year => Value.Year;
        public int Month => Value.Month;
        public DayOfWeek DayOfWeek { get; }
        public int DayOfYear => Value.DayOfYear;
        public int DayInMonth => _cal.GetDaysInMonth(Value.Year, Value.Month);
        public int DayInYear => _cal.GetDaysInYear(Value.Year);
        public int MonthsInYear => _cal.GetMonthsInYear(Value.Year);
        public int WeekOfYear => _cal.GetWeekOfYear(Value, _weekRule, DayOfWeek);
        public List<int[]> WeekMap { get; }
        public DateTime Value { get; }
        public CultureInfo Culture { get; }

        public override string ToString() => Value.ToString();
     
        public  string ToString(IFormatProvider provider) => Value.ToString(provider);
        public string ToString(string format) => Value.ToString(format);
        public string ToString(string format, IFormatProvider provider) => Value.ToString(format,provider);
        public DateTime this[int week, int day] => new DateTime(Year, Month, this.WeekMap[week][day]);
        public static bool operator >(Date first, Date second) => (DateTime)first > (DateTime)second;
        public static bool operator <(Date first, Date second) => (DateTime)first < (DateTime)second;
        public static int operator -(Date first, Date second) => Convert.ToInt32(((DateTime)first - (DateTime)second).TotalDays);
        public static Date operator +(Date date, int day) => new Date(date.Value.AddDays(day), date.Culture);
        public static Date operator -(Date date, int day) => new Date(date.Value.AddDays(day * -1), date.Culture);
        public static Date operator ++(Date date) => new Date(date.Value.AddDays(1), date.Culture);
        public static Date operator --(Date date) => new Date(date.Value.AddDays(-1), date.Culture);

        public static implicit operator DateTime(Date date) => date.Value;

        public static implicit operator Date(DateTime date) => new Date(date);
    }

    public static class DateExtensions
    {
        public static Date NextMonth(this Date date) =>date.AddMonth(1);
        public static Date PreviousMonth(this Date date) => date.AddMonth(-1);
        public static Date AddMonth(this Date date,int month) => new Date(date.Value.AddMonths(month));
    }
}
