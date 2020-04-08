using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeQ
{
    public interface IWeQuery
    {

        Task<bool> Popper(string target, string popper, ElementReference arrow, string placement);
        Task Native(string selector, string fn, params string[] args);

        Task _(string id, string selector, string fn, params string[] args);

        Task Css(string id, string selector, string css, string value);
        Task AddClass(string id, string selector, string className);
        Task RemoveClass(string id, string selector, string className);
        Task ToggleClass(string id, string selector, string className);
        Task SetCssLink(string id, string href);
        Task<string> GetCssLink(string id);

        Task Tooltip(string target, ElementReference tooltip, ElementReference arrow, string placement);

        Task Focus(string id, string selector);
    }
    public class WeQueryJS : IWeQuery
    {
        public WeQueryJS(IJSRuntime js)
        {
            this.JS = js;
        }

        public readonly IJSRuntime JS;
        public async Task<bool> Popper(string target, string popper, ElementReference arrow, string placement)
        {
            return await JS.InvokeAsync<bool>(WeQueryList.popper, target, popper, arrow, placement);
        }
        public async Task Native(string selector, string fn, params string[] args)
        {
            var p = new List<string>() { selector, fn };
            p.AddRange(args);
            await JS.InvokeVoidAsync(WeQueryList.native, p.ToArray());
        }

        public async Task _(string id, string selector, string fn, params string[] args)
        {
            var p = new List<string>() { id, selector, fn };
            p.AddRange(args);
            await JS.InvokeVoidAsync(WeQueryList._, p.ToArray());
        }

        public async Task Css(string id, string selector, string css, string value)
        {
            value = value[0].ToString().ToUpper() + value.Substring(1, value.Length - 1);
            await JS.InvokeVoidAsync(WeQueryList.Css, new object[] { id, selector, css, value });
        }
        public async Task AddClass(string id, string selector, string className)
        {
            await JS.InvokeVoidAsync(WeQueryList.AddClass, new object[] { id, selector, className });
        }
        public async Task RemoveClass(string id, string selector, string className)
        {
            await JS.InvokeVoidAsync(WeQueryList.RemoveClass, new object[] { id, selector, className });
        }

        public async Task ToggleClass(string id, string selector, string className)
        {
            await JS.InvokeVoidAsync(WeQueryList.ToggleClass, new object[] { id, selector, className });
        }

        public async Task SetCssLink(string id, string href)
        {
            await JS.InvokeVoidAsync(WeQueryList.setCssLink, id, href);
        }

        public async Task<string> GetCssLink(string id) => await JS.InvokeAsync<string>(WeQueryList.getCssLink, id);

        public async Task Tooltip(string target, ElementReference tooltip, ElementReference arrow, string placement)
        {
            await JS.InvokeVoidAsync(WeQueryList.tooltip, target, tooltip, arrow, placement);
        }

        public async Task Focus(string id, string selector) => await JS.InvokeVoidAsync(WeQueryList.Focus, new object[] { id, selector });
    }
}
