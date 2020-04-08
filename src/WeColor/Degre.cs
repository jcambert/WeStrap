using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace WeC
{
    [Serializable]
    [DebuggerDisplay("{m_value}")]
    public readonly struct Degre : IComparable, IComparable<Degre>, IConvertible, IEquatable<Degre>, IFormattable
    {
        //
        // Résumé :
        //     Represents the largest possible value of an Degre. This field is constant.
        public const double MaxValue = 360.0;
        //
        // Résumé :
        //     Represents the smallest possible value of Degre. This field is constant.
        public const double MinValue = 0.0;

        internal Degre(double value) => (this.m_value) = (value < MinValue ? MinValue : value > MaxValue ? MaxValue : value);
        internal Degre(short value) => (this.m_value) = (value < MinValue ? MinValue : value > MaxValue ? MaxValue : value);
        internal Degre(int value) => (this.m_value) = ((short)value <= MinValue ? MinValue : (short)value >= MaxValue ? MaxValue : (short)value);

        internal double m_value { get; }
        public int CompareTo(object value) =>
            value switch
            {
                null => 1,
                short _ => (int)(m_value - (short)value),
                int _ => (int)(m_value - (int)value),
                _ => throw new ArgumentException("object must be a number")
            };


        public int CompareTo([AllowNull] Degre other) => (int)(m_value - other.m_value);

        public bool Equals([AllowNull] Degre other) => other.m_value == m_value;

        public TypeCode GetTypeCode() => TypeCode.Int16;

        public bool ToBoolean(IFormatProvider provider) => Convert.ToBoolean(m_value, provider);

        public byte ToByte(IFormatProvider provider) => Convert.ToByte(m_value, provider);

        public char ToChar(IFormatProvider provider) => Convert.ToChar(m_value, provider);

        public DateTime ToDateTime(IFormatProvider provider) => throw new InvalidCastException("Cannot Cast Degre to DateTime");
        public decimal ToDecimal(IFormatProvider provider) => Convert.ToDecimal(m_value, provider);

        public double ToDouble(IFormatProvider provider) => Convert.ToDouble(m_value, provider);

        public short ToInt16(IFormatProvider provider) => Convert.ToInt16(m_value, provider);

        public int ToInt32(IFormatProvider provider) => Convert.ToInt32(m_value, provider);

        public long ToInt64(IFormatProvider provider) => Convert.ToInt64(m_value, provider);

        public sbyte ToSByte(IFormatProvider provider) => Convert.ToSByte(m_value, provider);

        public float ToSingle(IFormatProvider provider) => Convert.ToSingle(m_value, provider);

        public string ToString(IFormatProvider provider) => m_value.ToString(provider);

        public string ToString(string format, IFormatProvider provider) => m_value.ToString(format, provider);



        public object ToType(Type conversionType, IFormatProvider provider) => Convert.ChangeType(m_value, conversionType, provider);


        public ushort ToUInt16(IFormatProvider provider) => Convert.ToUInt16(m_value, provider);

        public uint ToUInt32(IFormatProvider provider) => Convert.ToUInt32(m_value, provider);

        public ulong ToUInt64(IFormatProvider provider) => Convert.ToUInt64(m_value, provider);

        public static implicit operator int(Degre value) => Convert.ToInt32(value.m_value);
        public static implicit operator Degre(double value) => new Degre(value);
        public static implicit operator Degre(int value) => new Degre(Convert.ToDouble(value));
        public static implicit operator Degre(short value) => new Degre(Convert.ToDouble( value));
        public static implicit operator Degre(string value)
        {
            value = value[^1] =='°' ? value[..^1] : value;
            if (string.IsNullOrEmpty(value))
                return new Degre(Degre.MinValue);
            if (double.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double doubleValue))
                return new Degre( doubleValue);
            throw new ArgumentException($"Impossible to parse {value} into degre");


        }
    }
}
