using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BamBurrito.Components.Layout
{
    public partial class NavItem
    {
        [Parameter] public string Href { get; set; } = string.Empty;
        [Parameter] public string? Icon { get; set; }
        [Parameter] public NavLinkMatch Match { get; set; } = NavLinkMatch.Prefix;
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }
    }
}