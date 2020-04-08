using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeCommon;
namespace WeStrap
{
    public abstract class WeDropdownBase : WeToggleableComponent
    {

        [Parameter] public EventCallback<WeDropdownEvent> ShowEvent { get; set; }
        [Parameter] public EventCallback<WeDropdownEvent> ShownEvent { get; set; }
        [Parameter] public EventCallback<WeDropdownEvent> HideEvent { get; set; }
        [Parameter] public EventCallback<WeDropdownEvent> HiddenEvent { get; set; }

        internal WeDropdownEvent BSDropdownEvent { get; set; }
        internal List<EventCallback<WeDropdownEvent>> EventQue { get; set; } = new List<EventCallback<WeDropdownEvent>>();

        // Prevents rogue closing
        private WeDropdownMenuBase _selected;

        private WeDropdownMenuBase _dropDownMenu { get; set; } = new WeDropdownMenu();
        public bool Active { get; set; } = false;
        private bool _shouldClose { get; set; } = false;

        internal WeDropdownMenuBase DropDownMenu
        {
            get => _dropDownMenu;
            set
            {
                _dropDownMenu = value;
                StateHasChanged();
            }
        }

        public WeDropdownMenuBase Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                if (_selected != null) FireChanged(true);
                InvokeAsync(StateHasChanged);
            }
        }

        protected void MouseLeave()
        {
            _shouldClose = true;
        }

        protected void MouseEnter()
        {
            _shouldClose = false;
        }
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
            .Add("dropdown", !IsGroup)
            .Add("btn-group", IsGroup)
            .Add("dropdown-submenu", IsSubmenu)
            .Add(DropdownDirection.ToDescriptionString(), DropdownDirection != DropdownDirection.Down)
            .Add("show", Selected != null || IsOpen)
            .Add("active", Active);
        }


        internal bool IsSubmenu { get; set; }
        [Parameter] public DropdownDirection DropdownDirection { get; set; } = DropdownDirection.Down;
        [Parameter] public bool IsGroup { get; set; }
        [CascadingParameter] protected WeDropdown Dropdown { get; set; }
        [CascadingParameter] internal WeNavItem NavItem { get; set; }

        protected override void OnInitialized()
        {
            if (Dropdown != null || NavItem != null)
            {
                IsSubmenu = true;
            }
            /*BlazorStrapInterop.OnAnimationEndEvent += OnAnimationEnd;*/
            base.OnInitialized();
        }

        private async Task OnAnimationEnd(string id)
        {
            BSDropdownEvent = new WeDropdownEvent() { Target = this };
            if (id != MyRef.Id)
            {
                if (IsOpen)
                {
                    await ShownEvent.InvokeAsync(BSDropdownEvent).ConfigureAwait(false);
                }
                else
                {
                    await HiddenEvent.InvokeAsync(BSDropdownEvent).ConfigureAwait(false);
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //BlazorStrapInterop.OnAnimationEndEvent -= OnAnimationEnd;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /*  internal override async Task Changed(bool e)
          {
              BSDropdownEvent = new WeDropdownEvent() { Target = this };
              if (e)
              {
                  await ShowEvent.InvokeAsync(BSDropdownEvent).ConfigureAwait(false);
              }
              else
              {
                  await HideEvent.InvokeAsync(BSDropdownEvent).ConfigureAwait(false);
              }
          }*/

        protected void LostFocus()
        {
            if (_shouldClose)
            {
                Close();
            }
        }

        protected void Close()
        {
            Selected = null;
        }

        protected override Task OnAfterRenderAsync(bool firstrun)
        {
            for (var i = 0; i < EventQue.Count; i++)
            {
                EventQue[i].InvokeAsync(BSDropdownEvent);
                EventQue.RemoveAt(i);
            }
            return base.OnAfterRenderAsync(false);
        }
    }
}
