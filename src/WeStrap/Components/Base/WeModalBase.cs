using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using WeCommon;
namespace WeStrap
{
    public abstract class WeModalBase : WeToggleableComponent
    {

        [Parameter] public EventCallback<WeEvent<WeModalBase>> HiddenEvent { get; set; }
        [Parameter] public EventCallback<WeEvent<WeModalBase>> HideEvent { get; set; }
        [Parameter] public bool IgnoreClickOnBackdrop { get; set; }
        [Parameter] public bool IgnoreEscape { get; set; }
        [Parameter] public bool IsCentered { get; set; }
        [Parameter] public bool NoBackdrop { get; set; }
        [Parameter] public EventCallback<WeEvent<WeModalBase>> ShowEvent { get; set; }
        [Parameter] public EventCallback<WeEvent<WeModalBase>> ShownEvent { get; set; }
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public string Style { get; set; }
        internal WeEvent<WeModalBase> WeModalEvent { get; set; }
        // internal List<EventCallback<WeEvent<WeModalBase>>> EventQue { get; set; } = new List<EventCallback<WeEvent<WeModalBase>>>();
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)

                .Add("modal")
                .Add("fade")
                .Add("show", Open);
        }


        protected string InnerClassname =>
            new WeStringBuilder()
            .Add("modal-dialog")
                .Add($"modal-{Size.ToDescriptionString()}", Size != Size.None)
                .Add("modal-dialog-centered", IsCentered)
            .ToString();

        //  [Inject] protected Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }

        protected WeQ.WeQuery query { get; set; }

        protected ElementReference Me { get; set; }
        protected string Styles
        {
            get
            {
                //   var display = DisableAnimations || (IsOpen ?? true)
                //   ? (IsOpen ?? false) ? "display: block; padding-right: 17px;" : null
                var display = (_toggleModel) ? "display: block; padding-right: 17px;" : null;
                return $"{Style} {display}".Trim();
            }
        }

        private bool _toggleShow { get; set; }
        private bool _toggleModel { get; set; }
        private bool _isInitialized { get; set; }

        /*  internal override async Task Changed(bool e)
          {
              if (!_isInitialized)
              {
                  return;
              }

              WeModalEvent = new WeEvent<WeModalBase>(this);
              if (e)
              {
                  await query.AddClass("div.modal-backdrop", "show");
                  await query.AddClass("div.modal", "show");
                  //await new BlazorStrapInterop(JSRuntime).AddBodyClass("modal-open");
                  if (!IgnoreEscape)
                  {

                      //await new BlazorStrapInterop(JSRuntime).ModalEscapeKey();
                      //BlazorStrapInterop.OnEscapeEvent += OnEscape;
                  }
                  new Task(async () =>
                  {
                      _toggleModel = true;
                      await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                      await Task.Delay(300).ConfigureAwait(false);
                      _toggleShow = e;
                      await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                  }).Start();
                  await ShowEvent.InvokeAsync(WeModalEvent).ConfigureAwait(false);
              }
              else
              {
                  new Task(async () =>
                  {
                      _toggleShow = e;
                      await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                      await Task.Delay(300).ConfigureAwait(false);
                      _toggleModel = false;
                      await InvokeAsync(StateHasChanged).ConfigureAwait(false);
                  }).Start();
                  await HideEvent.InvokeAsync(WeModalEvent).ConfigureAwait(false);
                  // await new BlazorStrapInterop(JSRuntime).RemoveBodyClass("modal-open");
                  await query.RemoveClass("div.modal", "show");
                  await query.RemoveClass("div.modal-backdrop", "show");
              }
          }*/

        protected override Task OnAfterRenderAsync(bool firstrun)
        {

            if (firstrun)
            {
                _isInitialized = true;
            }


            return base.OnAfterRenderAsync(false);
        }

        protected void OnBackdropClick()
        {
            if (!IgnoreClickOnBackdrop)
            {
                Hide();
                StateHasChanged();
            }
        }

        protected async Task OnEscape()
        {
            Hide();
            //BlazorStrapInterop.OnEscapeEvent -= OnEscape;
            await InvokeAsync(StateHasChanged).ConfigureAwait(false);
        }

        protected override void OnInitialized()
        {
            AnimationClass = AnimationClass ?? "fade";
            OnChanged.Subscribe(async (state) =>
            {
                if (state == ToggleState.Open)
                {
                    await query.Query.Native("body", "addClass", "modal-open");
                    //await query.AddClass("div.modal-backdrop", "show");
                    // await query.AddClass("div.modal", "show");
                }
                else
                {
                    await query.Query.Native("body", "removeClass", "modal-open");
                    //await query.RemoveClass("div.modal-backdrop", "show");
                    //await query.RemoveClass("div.modal", "show");
                }
            });
            base.OnInitialized();
        }
    }
}
