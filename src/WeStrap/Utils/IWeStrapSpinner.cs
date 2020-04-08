using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WeStrap
{
    public readonly struct Spinner
    {
        public Spinner(int nb, string @class, string inner, bool colorInner)
        {
            this.NumberOfElement = nb;
            this.Class = @class;
            this.InnerClass = inner;
            this.ApplyColorToInner = colorInner;

        }
        public int NumberOfElement { get; }
        public string Class { get; }
        public string InnerClass { get; }
        public bool ApplyColorToInner { get; }
    }
    public interface IWeStrapSpinner
    {
        void Add(string name, Spinner spinner);
        Spinner this[string name] { get; }
        IEnumerable<string> Names { get; }

        string Default{get;}
    }

    public sealed class WeStrapSpinner : IWeStrapSpinner
    {
        const string DEFAULT = "Circle Fade";
        Dictionary<string, Spinner> _defaults = new Dictionary<string, Spinner>
        {
                {"Circle Dot",new Spinner(12,"circle","circle-dot",true) },
                {"Circle Fade",new Spinner(12,"circle","circle-fade",true) },
                {"Cube Grid",new Spinner(9,"cube-grid","cube",false) },
                {"Cube Fold",new Spinner(4,"cube-fold","cube",true) },
                {"Cube Wandering",new Spinner(2,"cube-wandering","cube",false) },
                {"Cube Wave",new Spinner(5,"cube-wave","cube",false) },
                {"Plane",new Spinner(0,"rotating-plane","",false) },
                {"Chasing Dot",new Spinner(2,"chasing-dots","dot",false) },
                {"Double Bounce",new Spinner(2,"double-bounce","double",false) },
                {"Three Bounce",new Spinner(3,"three-bounce","three",false) },
                {"Pulse",new Spinner(0,"pulse","",false) },
            };
        Dictionary<string, Spinner> _users = new Dictionary<string, Spinner>();

        public void Add(string name,Spinner spinner)
        {
            if (_users.ContainsKey(name) || _users.ContainsKey(name))
                throw new ArgumentException($"The Spinner with name {name} already exist");
            _users[name] = spinner;
        }

        public Spinner this[string name]
            => string.IsNullOrEmpty(name)?_defaults[DEFAULT]:  _defaults.ContainsKey(name) ? _defaults[name] : _users.ContainsKey(name) ? _users[name] : _defaults[DEFAULT ];

        public IEnumerable<string> Names => _users.Keys.Concat(_defaults.Keys);

        public string Default => DEFAULT;
    }
}
