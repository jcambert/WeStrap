using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace WeC
{
    /// <summary>
    /// Represente un entier non signe de 0 à 255
    /// </summary>

    [Serializable]
    [DebuggerDisplay("{m_value}")]
    public readonly struct IntH : IComparable, IComparable<IntH>, IConvertible, IEquatable<IntH>, IFormattable
    {

        //
        // Résumé :
        //     Represents the largest possible value of an IntH. This field is constant.
        public const Int16 MaxValue = 255;
        //
        // Résumé :
        //     Represents the smallest possible value of Inth. This field is constant.
        public const Int16 MinValue = 0;

        internal IntH(short value) => (this.m_value) = (value < MinValue ? MinValue : value > MaxValue ? MaxValue : value);
        internal IntH(int value) => (this.m_value) = ((short)value <= MinValue ? MinValue : (short)value >= MaxValue ? MaxValue : (short)value);

        internal short m_value { get; }
        public int CompareTo(object value) =>
            value switch
            {
                null => 1,
                short _ => (int)(m_value - (short)value),
                int _ => (int)(m_value - (int)value),
                _ => throw new ArgumentException("object must be a number")
            };


        public int CompareTo([AllowNull] IntH other) => (int)(m_value - other.m_value);

        public bool Equals([AllowNull] IntH other) => other.m_value == m_value;

        public TypeCode GetTypeCode() => TypeCode.Int16;

        public bool ToBoolean(IFormatProvider provider) => Convert.ToBoolean(m_value, provider);

        public byte ToByte(IFormatProvider provider) => Convert.ToByte(m_value, provider);

        public char ToChar(IFormatProvider provider) => Convert.ToChar(m_value, provider);

        public DateTime ToDateTime(IFormatProvider provider) => throw new InvalidCastException("Cannot Cast IntH to DateTime");
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
        public static implicit operator IntH(Percentage value)=> ((int)value.m_value * IntH.MaxValue);
        public static implicit operator int(IntH value) => Convert.ToInt32(value.m_value);
        public static implicit operator IntH(double value)=>new IntH(Convert.ToInt32(value));
        public static implicit operator IntH(int value) => new IntH(Convert.ToInt16(value));
        public static implicit operator IntH(short value) => new IntH(value);
        public static implicit operator IntH(string value)
        {
            if (string.IsNullOrEmpty(value))
                return new IntH(IntH.MaxValue);
            if (double.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double doubleValue))
                if (doubleValue >= 0 && doubleValue <= 1.0)
                    return new IntH(Convert.ToInt16(IntH.MaxValue * doubleValue));
                else
                    return (IntH)doubleValue;
            if (value[^1] == '%')
                return new IntH(IntH.MaxValue * Convert.ToInt16(value[..^1]) / 100);

            return new IntH(value.StartsWith("0x") ? Convert.ToInt16(value, 16) : int.Parse(value, NumberStyles.HexNumber));


        }
    }
}
