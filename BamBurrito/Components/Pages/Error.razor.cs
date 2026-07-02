using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace BamBurrito.Components.Pages
{
    public partial class Error
    {
        [CascadingParameter] private HttpContext? HttpContext { get; set; }

        protected string? RequestId { get; set; }
        protected bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        protected override void OnInitialized()
        {
            RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
        }
    }
}