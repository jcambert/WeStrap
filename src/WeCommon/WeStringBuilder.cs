using System;
using System.Collections.Generic;
using System.Linq;

namespace WeCommon
{
    public sealed class WeStringBuilder
    {
        private readonly List<string> buffer = new List<string>();
        private string _prefix = string.Empty;
        private bool _immediate = false;
        public WeStringBuilder(params string[] values)
        {
            buffer.AddRange(values);
        }
        public void Visit(Action<string> action, int index = -1)
        {
            if (index == -1)
                foreach (var item in buffer)
                {
                    action?.Invoke(item);
                }
            else if(index<buffer.Count)
            {
                action?.Invoke(buffer[index]);
            }
        }
        internal List<string> Buffer => buffer;
        private string format(string value)
        {
            if (string.IsNullOrEmpty(_prefix)) return value.Trim();
            return string.Format(_prefix, value.Trim());
        }
        public WeStringBuilder Immediate(bool value)
        {
            _immediate = value;
            return this;
        }
        public WeStringBuilder Add(string value)
        {
            value = value ?? string.Empty;
            if (value.Trim().Length > 0)
                if (_immediate && buffer.Any())
                    buffer[buffer.Count - 1] = buffer.Last() + format(value);
                else
                    buffer.Add(format(value));
            return this;
        }
        public WeStringBuilder Add(char value) => Add(value.ToString());
        public WeStringBuilder Add(IEnumerable<string> values)
        {
            buffer.AddRange(values.Where(v => v.Trim().Length > 0).Select(v => format(v)));
            return this;
        }
        public WeStringBuilder Add(string value, bool when = true) => when ? Add(value) : this;

        public WeStringBuilder Add(string value, Func<bool> when = null) => this.Add(value, when());

        public WeStringBuilder Add(Func<string> value, bool when = true) => this.Add(value(), when);

        public WeStringBuilder Add(Func<string> value, Func<bool> when = null) => this.Add(value, when());

        public WeStringBuilder Add(WeStringBuilder builder, bool when = true) => when ? this.Add(builder.Buffer) : this;

        public WeStringBuilder Add(WeStringBuilder builder, Func<bool> when = null) => this.Add(builder, when());
        public WeStringBuilder Add( IEnumerable<string> liste, bool when = true)
        {
            foreach (var item in liste)
            {
                Add(item, when);
            }
            return this;
        }
        public WeStringBuilder Add<T>(IEnumerable<T> liste, bool when = true)
        {
            foreach (var item in liste)
            {
                Add(item.ToString(), when);
            }
            return this;
        }
        public WeStringBuilder AddList<T>(string value, IEnumerable<T> liste, bool when = true)
            where T : struct
        {
            foreach (var item in liste)
            {
                Add(string.Format(value, item.ToDescriptionString()), when);
            }
            return this;
        }
        public WeStringBuilder AddList(string value, IEnumerable<string> liste, bool when = true)
        {
            foreach (var item in liste)
            {
                Add(string.Format(value, item), when);
            }
            return this;
        }
        public WeStringBuilder UsePrefix(string prefix, bool when = true)
        {
            _prefix = when ? (prefix ?? string.Empty) : string.Empty;
            return this;
        }
        public WeStringBuilder RemovePrefix()
        {
            _prefix = String.Empty;
            return this;
        }
        public override string ToString() => Join(" ");

        public string Join(string separator = "") => string.Join(separator, Buffer);

        public bool HasItems => buffer.Any();

        public string[] ToArray() => Buffer.ToArray();
        public static implicit operator string( WeStringBuilder sb)=>sb.ToString();
    }
}
