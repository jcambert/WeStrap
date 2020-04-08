using Microsoft.AspNetCore.Components.Web;
using System;

namespace WeStrap
{
    public class WeEvent<T> : EventArgs
    {
        public WeEvent(T target, MouseEventArgs args = null)
        {
            Target = target;
            MouseEventArgs = args;
        }
        public T Target { get; private set; }

        public MouseEventArgs MouseEventArgs { get; private set; }
    }
}
