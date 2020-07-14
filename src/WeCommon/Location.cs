using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using static System.Math;
namespace WeCommon
{
    [BsonSerializer(typeof(BsonLocationSerializer))]
    [JsonConverter(typeof(JsonLocationSerializer))]
    
    [DebuggerDisplay("Lon:{Longitude} - Lat:{Latitude}")]
    public readonly struct Location : IComparable<Location>, IEquatable<Location>
    {
        [JsonConstructor]
        public Location(double lon, double lat)
            => (Longitude, Latitude) = (lon, lat);

        public readonly double Longitude { get; }

        public readonly double Latitude { get; }

        #region IComparable
        public int CompareTo([AllowNull] Location other)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region IEquatable
        public bool Equals([AllowNull] Location other) => other == null ? false : (Latitude == other.Latitude && Longitude == other.Longitude);

        public override bool Equals(object obj) => this.Equals(obj as Nullable<Location>);

        public override int GetHashCode() => (Longitude.GetHashCode() << 5) + 3 + Longitude.GetHashCode() ^ Latitude.GetHashCode();
        public static bool operator ==(Location left, Location right) => left.Equals(right);

        public static bool operator !=(Location left, Location right) => !(left == right);

        public static double operator -(Location left, Location right) => left.CalculateDistance(right);

        #endregion
    }

    public static class LocationExtensions
    {
        public static double CalculateDistance(this Location start, Location end)
        {
            if (start == end) return 0;

            var rlat1 = PI * start.Latitude / 180.0;
            var rlat2 = PI * end.Latitude / 180.0;
            var theta = start.Longitude - end.Longitude;
            var rtheta = PI * theta / 180.0;
            var dist = Sin(rlat1) * Sin(rlat2) + Cos(rlat1) * Cos(rlat2) * Cos(rtheta);
            dist = Acos(dist);
            dist = dist * 180.0 / PI;
            var final = dist * 60.0 * 1.1515;
            if (double.IsNaN(final) || double.IsInfinity(final) || double.IsNegativeInfinity(final) ||
                double.IsPositiveInfinity(final) || final < 0)
                return 0;

            return final;
        }
    }
}
