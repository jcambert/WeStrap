using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using WeQ;
namespace WeStrap
{
    public class CurrentTheme
    {
        public static IEnumerable<Theme> Themes = Enum.GetValues(typeof(Theme)).Cast<Theme>();


        public CurrentTheme()
        {

        }
        internal bool FirstRender { get; set; }
        private Theme _theme;
        private readonly ISubject<Theme> _onChanged = new Subject<Theme>();
        private readonly ISubject<Theme> _onBeforeChanged = new Subject<Theme>();
        public IObservable<Theme> OnChanged => _onChanged.AsObservable();
        public IObservable<Theme> OnBeforeChanged => _onBeforeChanged.AsObservable();
        public Theme Theme
        {
            get => _theme;
            set
            {
                if (value != _theme)
                {
                    _onBeforeChanged.OnNext(_theme);
                    _theme = value;
                    _onChanged.OnNext(value);
                }
            }
        }
    }
    public interface IWeStrapTheme
    {
        Theme Theme { get; }
        //Task SetWeStrapTheme();
        //Task SetWeStrapTheme(string theme);
        //Task SetWeStrapTheme(Theme theme);
        Task FindCurrentTheme(bool firstRender);
    }

    class WeStrapTheme : IWeStrapTheme, IDisposable
    {
        private static string CSS_ID = "westrap-theme";
        private static string CSS_HREF = "_content/WeStrap/css/{0}.css";
        private CurrentTheme _currentTheme;
        private readonly IDisposable _obs;
        private readonly IWeQuery _query;
        public WeStrapTheme(IWeQuery query, CurrentTheme theme)
        {
            this._query = query;
            this._currentTheme = theme;
            this._obs = this._currentTheme.OnChanged.Subscribe(async (theme) =>
              {
                  await SetWeStrapTheme(theme);
              });
            // Task.Run(() => FindCurrentTheme());
        }
        public Theme Theme => _currentTheme.Theme;

        internal Task SetWeStrapTheme()
        {
            return SetWeStrapTheme(Theme.Bootstrap);
        }

        internal Task SetWeStrapTheme(string theme)
        {
            throw new NotImplementedException();
        }

        internal async Task SetWeStrapTheme(Theme theme)
        {
            //_currentTheme.Theme = Theme.Bootstrap;
            await _query.SetCssLink(CSS_ID, string.Format(CSS_HREF, Theme.ToString().ToLowerInvariant()));
            //await JS.InvokeVoidAsync("window.wequery.setCssLink", "westrap-theme", $"_content/WeStrap/css/{Theme.ToString().ToLowerInvariant()}");
        }

        public async Task FindCurrentTheme(bool firstRender)
        {
            _currentTheme.FirstRender = firstRender;
            try
            {
                //string href = await JS.InvokeAsync<string>( "window.wequery.getCssLink", "westrap-theme");
                string href = await _query.GetCssLink(CSS_ID);
                string stheme = Enum.GetNames(typeof(Theme)).ToList().Where(t => href == string.Format(CSS_HREF, t.ToString().ToLowerInvariant())).FirstOrDefault();
                if (Enum.TryParse(typeof(Theme), stheme, out object theme))
                {
                    _currentTheme.Theme = (Theme)theme;
                }
            }
            finally
            {
                _currentTheme.FirstRender = !_currentTheme.FirstRender;
            }
        }
        #region IDisposable Support
        private bool disposedValue = false; // Pour détecter les appels redondants

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés).
                    this._obs.Dispose();
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~WeStrapTheme()
        // {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
