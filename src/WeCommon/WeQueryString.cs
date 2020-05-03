using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace WeCommon
{
    public readonly struct WeQueryString : IEquatable<WeQueryString>
    {
        public static readonly WeQueryString Empty = new WeQueryString(string.Empty);

        private readonly Dictionary<string, string>  _dico ;
        private readonly string _value;
        private WeQueryString(string value)
        {
            this._value = value ?? string.Empty;
            _dico = new Dictionary<string, string>();
            this._value.QueryStringToDictionary(_dico);
            
        }
        public WeQueryString Add(string value)
        {
            value.QueryStringToDictionary(_dico, true);
            return this;
        }
        public WeQueryString Add(IEnumerable<KeyValuePair<string,string>> values)
        {
            foreach (var value in values)
            {
                Add(value.Key, value.Value);
            }
            return this;
        }
        public WeQueryString Add(string key,string value)
        {
            if (key == null) throw new ArgumentNullException($"WeQueryString.Add {nameof(key)} cannot be null");
            _dico[key] = value ?? string.Empty;
            return this;
        }
        public WeQueryString Add(WeQueryString qs)
        {
            foreach (var key in qs.Keys)
            {
                Add(key, qs[key]);
            }
            return this;
        }
        public WeQueryString Remove(string key)
        {
            if (key == null) return this;
            if (_dico.ContainsKey(key))
                _dico.Remove(key);
            return this;
        }
        public string this[string key]=>_dico[key];

        public List<string> Keys => _dico.Keys.ToList();
        public bool HasValue => _dico.Keys.Any();

        public static WeQueryString Create(string value) => new WeQueryString(value);

        public static WeQueryString Create(IEnumerable<KeyValuePair<string, string>> values) => new WeQueryString(null).Add(values);

        public override string ToString()
        {
            WeStringBuilder sb = new WeStringBuilder();
            foreach (var key in Keys)
            {
                sb.Add($"{UrlEncoder.Default.Encode(key)}={UrlEncoder.Default.Encode(_dico[key])}");
            }
            return sb.Join("&");
        }

        public bool Equals([AllowNull] WeQueryString other)
        {
            if (!HasValue && !other.HasValue) return true;
            return string.Equals(_value, other._value, StringComparison.Ordinal);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return !HasValue;
            }
            return obj is WeQueryString && Equals((WeQueryString)obj);
        }
        public override int GetHashCode() => (HasValue ? _value.GetHashCode() : 0);
        public static bool operator ==(WeQueryString left, WeQueryString right)=>left.Equals(right);
        
        public static bool operator !=(WeQueryString left, WeQueryString right)=>!left.Equals(right);

        public static WeQueryString operator +(WeQueryString left, WeQueryString right)=> left.Add(right);

        public static WeQueryString operator +(WeQueryString left, string right) => left.Add(right);

        public static implicit  operator string(WeQueryString value)=>value.ToString();
        public static implicit operator WeQueryString(string value) => WeQueryString.Create(value);
    }
}
