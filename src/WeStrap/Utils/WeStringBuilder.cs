namespace WeStrap
{
    /*public class WeStringBuilder
    {
        private List<string> buffer=new List<string>();
        private string _prefix = string.Empty;
        public WeStringBuilder()
        {
           
        }

        public List<string> Buffer => buffer;
        private string format(string value)
        {
            if (string.IsNullOrEmpty(_prefix)) return value.Trim();
            return string.Format(_prefix, value.Trim());
        }
        public WeStringBuilder Add(string value)
        {
            value = value ?? string.Empty;
            if(value.Trim().Length>0)
                buffer.Add(format(value));
            return this;
        }
        public WeStringBuilder Add(IEnumerable<string> values)
        {
            buffer.AddRange(values.Where(v=>v.Trim().Length>0).Select(v=>format(v)));
            return this;
        }
        public WeStringBuilder Add(string value, bool when = true) => when ? Add(value) : this;

        public WeStringBuilder Add(string value, Func<bool> when = null) => this.Add(value, when());

        public WeStringBuilder Add(Func<string> value, bool when = true) =>this.Add(value(),when) ;

        public WeStringBuilder Add(Func<string> value, Func<bool> when = null) => this.Add(value, when());

        public WeStringBuilder Add(WeStringBuilder builder, bool when = true) => when ? this.Add(builder.Buffer) : this;

        public WeStringBuilder Add(WeStringBuilder builder, Func<bool> when = null) => this.Add(builder, when());

        public WeStringBuilder UsePrefix(string prefix,bool when = true){
            _prefix = when ? prefix : string.Empty;
            return this;
        }
        public WeStringBuilder RemovePrefix()
        {
            _prefix = String.Empty;
            return this;
        }
        public override string ToString()
        {
            return string.Join(" ", buffer);
        }
    }*/
}
