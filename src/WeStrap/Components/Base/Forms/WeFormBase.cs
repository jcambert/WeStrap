using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using WeQ;

namespace WeStrap
{
    public abstract partial class WeFormBase<TModel> : WeTag, IWeForm
        where TModel : class, new()
    {
        private readonly Func<Task> _handleSubmitDelegate; // Cache to avoid per-render allocations

        public WeFormBase()
        {
            _handleSubmitDelegate = HandleSubmitAsync;
        }

        public IWeEditContext GetEditContext() => EditContext;
        public WeEditContext<TModel> EditContext { get; private set; }
        public Type ModelType => typeof(TModel);

        [Inject] public IWeQuery JQuery { get; set; }
        [Parameter] public TModel Model { get; set; } = new TModel();

        [Parameter] public bool UserValidation { get; set; }
        [Parameter] public bool ValidateOnInit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted.
        ///
        /// If using this parameter, you are responsible for triggering any validation
        /// manually, e.g., by calling <see cref="EditContext.Validate"/>.
        /// </summary>
        [Parameter] public EventCallback<WeEditContext<TModel>> OnSubmit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted and the
        /// <see cref="EditContext"/> is determined to be valid.
        /// </summary>
        [Parameter] public EventCallback<WeEditContext<TModel>> OnValidSubmit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted and the
        /// <see cref="EditContext"/> is determined to be invalid.
        /// </summary>
        [Parameter] public EventCallback<WeEditContext<TModel>> OnInvalidSubmit { get; set; }

        public bool HasValidationErrors => EditContext.GetValidationMessages().Any();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine("***************************");
            if (firstRender)
            {
                Console.WriteLine("PRE-RENDERING");
            }
            else
            {
                Console.WriteLine("RENDERING");
            }
            Console.WriteLine("Start Rendering Form");
            base.OnAfterRender(firstRender);

           // await SetFocus();
            Console.WriteLine("Form Rendered ");
        }

        IDisposable d1, d2;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            EditContext = new WeEditContext<TModel>(this, Model);
            // if (ValidateOnInit) ForceValidate();
            d1=EditContext.OnValidationStateChanged().Subscribe(args =>
                {
                    //if (!EditContext.IsValid())
                      //  InvokeAsync(() => SetFocus());
                });
            d2=EditContext.OnFormValid().Subscribe(args =>
            {
                //InvokeAsync(() => JQuery.Native("form button[type='submit']:first", "focus"));
            });
        }
        protected override void OnParametersSet()
        {
        }

        private List<IWeInputBase> ComponentChilds = new List<IWeInputBase>();
        public void AddInputChild(IWeInputBase component)
        {
            if (component == null) return;
            Console.WriteLine($"Form Add Child->{component.Id}");
            ComponentChilds.Add(component);
            component.TabIndex = ComponentChilds.Count();
        }

        protected async Task SetFocus()
        {

            Console.WriteLine($"Setting focus");
            await JQuery.Native("input.is-invalid:first", "focus");


        }
        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {



            builder.OpenRegion(EditContext.GetHashCode());

            builder.OpenElement(0, "form");
            builder.AddMultipleAttributes(1, UnknownParameters);
            builder.AddAttribute(2, "onsubmit", _handleSubmitDelegate);

            builder.OpenComponent<CascadingValue<WeEditContext<TModel>>>(3);
            builder.AddAttribute(4, "IsFixed", true);
            builder.AddAttribute(5, "Value", EditContext);
            builder.AddAttribute(6, "ChildContent", (RenderFragment)(b => ChildContent?.Invoke(b)));

            builder.CloseComponent();
            builder.CloseElement();

            builder.CloseRegion();
        }

        private async Task HandleSubmitAsync()
        {
            if (OnSubmit.HasDelegate)
            {
                // When using OnSubmit, the developer takes control of the validation lifecycle
                await OnSubmit.InvokeAsync(EditContext);
            }
            else
            {
                // Otherwise, the system implicitly runs validation on form submission
                var isValid = EditContext.Validate(); // This will likely become ValidateAsync later

                if (isValid && OnValidSubmit.HasDelegate)
                {
                    await OnValidSubmit.InvokeAsync(EditContext);
                }

                if (!isValid && OnInvalidSubmit.HasDelegate)
                {
                    await OnInvalidSubmit.InvokeAsync(EditContext);
                }
            }
        }

        public void ForceValidate()
        {
            if (!UserValidation)
                InvokeAsync(() =>
                {
                    EditContext.Validate();
                    // StateHasChanged();
                });
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
                    d1?.Dispose();
                    d2?.Dispose();
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~WeFormBase()
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
