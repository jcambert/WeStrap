using System.Collections.Generic;

namespace WeCommon
{
    public readonly struct Periode
    {
        public Periode(Date start, Date end) =>
            (Start, End) = (start < end ? start : end, end < start ? start : end);


        public readonly Date Start { get; }
        public readonly Date End { get; }
        public Date this[int offset] => Start + offset;

        public int GetDays() => End - Start;
        public IEnumerable<Date> GetDates()
        {
            for (int i = 0; i < GetDays(); i++)
            {
                yield return this[i];
            }
        }
    }

}
