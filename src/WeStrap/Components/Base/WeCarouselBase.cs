using Microsoft.AspNetCore.Components;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using WeCommon;
namespace WeStrap
{
    public abstract partial class WeCarouselBase : WeTag, IDisposable
    {


        protected override void OnInitialized()
        {
            base.OnInitialized();
            Start();
        }

        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
                .Add("carousel")
                .Add("slide")
                .Add("carousel-fade", Fade);
        }
        public void Pause()
        {
            _timer.Dispose();
            _timer = null;
        }

        public void Start()
        {
            CreateTimer();
        }
        private void CreateTimer()
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
            if (Interval > 0)
                _timer = Observable.Interval(TimeSpan.FromSeconds(Interval)).Subscribe(x => { WantSlide(Direction); });


        }
        private IDisposable _timer;

        [Parameter] public bool Fade { get; set; } = false;
        private int _interval = 3;
        [Parameter]
        public int Interval
        {
            get => _interval;
            set
            {
                if (_interval != value)
                {
                    _interval = value;
                    CreateTimer();
                }
            }
        }

        private readonly Subject<CarouselDirection> _wantSlideDirection = new Subject<CarouselDirection>();
        private readonly Subject<int> _wantSlideIndex = new Subject<int>();
        private readonly Subject<int> _activeIndexChanged = new Subject<int>();

        private CarouselDirection _direction = CarouselDirection.Next;
        [Parameter]
        public CarouselDirection Direction
        {
            get => _direction;
            set => _direction = value;
        }
        public IObservable<CarouselDirection> SlideTo => _wantSlideDirection.AsObservable();
        public IObservable<int> SlideToIndex => _wantSlideIndex.AsObservable();
        public IObservable<int> ActiveIndexChanged => _activeIndexChanged.AsObservable();
        public void WantSlide(CarouselDirection direction)
        {
            Direction = direction;
            _wantSlideDirection.OnNext(direction);
        }

        public void WantSlide(int index)
        {
            _wantSlideIndex.OnNext(index);
        }

        public void ChangeActiveIndex(int index)
        {
            // Console.WriteLine($"Change Active Index to {index}");
            _activeIndexChanged.OnNext(index);
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
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~WeCarouselBase()
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

