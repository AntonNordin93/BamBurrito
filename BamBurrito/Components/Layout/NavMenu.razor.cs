using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace BamBurrito.Components.Layout
{
    public partial class NavMenu : IDisposable
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;
        [Inject] public IJSRuntime JSRuntime { get; set; } = null!;

        private string? currentUrl;
        private bool collapseNavMenu = true;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        private void CloseNavMenu()
        {
            collapseNavMenu = true;
        }

        protected override void OnInitialized()
        {
            currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
            NavigationManager.LocationChanged += OnLocationChanged;
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            currentUrl = NavigationManager.ToBaseRelativePath(e.Location);
            collapseNavMenu = true;
            StateHasChanged();
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}