using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using WeCommon;
namespace WeStrap
{
    public abstract class WeContainerBase : WeTag
    {
        [Parameter] public bool IsFluid { get; set; }
        [Inject] public WeQ.IWeQuery Query { get; set; }
        protected override WeStringBuilder BuildClassName(string s = "")
        {
            return base.BuildClassName(s)
            .Add("container", !IsFluid)
            .Add("container-fluid", IsFluid);
        }
        private List<WePopoverBase> popovers = new List<WePopoverBase>();
        internal void RegisterPopover(WePopoverBase popover)
        {
            if (popovers.Contains(popover)) return;
            popovers.Add(popover);
            popover.OnChanged.Subscribe(state =>
            {
                if (state == ToggleState.Open)
                {
                    Console.WriteLine("######################");
                    Console.WriteLine($"{popover.Target} is opened");
                    foreach (var poper in popovers.Where(p => p != popover))
                    {
                        Console.WriteLine($"{poper.Target} is closed");
                        poper.Hide();
                    }
                }

            });
        }
    }
}
