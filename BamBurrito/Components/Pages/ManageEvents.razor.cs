using Microsoft.AspNetCore.Components;
using BamBurrito.Core.Entities;
using BamBurrito.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.WebUtilities;

namespace BamBurrito.Components.Pages;

public partial class ManageEvents
{
    [Inject] private LocationService LocationService { get; set; } = default!;
    [Inject] private NavigationManager Navigation { get; set; } = default!;
    [Inject] private IConfiguration Configuration { get; set; } = default!;

    [SupplyParameterFromForm] // Detta är magin som binder formuläret
    private LocationEvent newEvent { get; set; } = new() { EventDate = DateTime.Now };

    private bool isAuthorized = false;
    private List<LocationEvent> events = new();

    protected override void OnInitialized()
    {
        var uri = Navigation.ToAbsoluteUri(Navigation.Uri);
        var query = QueryHelpers.ParseQuery(uri.Query);
        var secret = Configuration["AdminSecretKey"];

        if (query.TryGetValue("key", out var key) && key == secret)
            isAuthorized = true;
        else
            Navigation.NavigateTo("/");
    }

    protected override async Task OnInitializedAsync()
    {
        if (isAuthorized) events = await LocationService.GetEventsAsync();
    }

    private async Task HandleSubmit()
    {
        await LocationService.CreateEventAsync(newEvent);

        // 1. Hämta listan på nytt
        events = await LocationService.GetEventsAsync();

        // 2. Nollställ formuläret
        newEvent = new() { EventDate = DateTime.Now };

        // 3. TVINGA UI att rita om sig!
        StateHasChanged();
    }
}