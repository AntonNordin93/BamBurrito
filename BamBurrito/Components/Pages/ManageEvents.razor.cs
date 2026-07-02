using Microsoft.AspNetCore.Components;
using BamBurrito.Core.Entities;
using BamBurrito.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BamBurrito.Components.Pages;

public partial class ManageEvents
{
    [Inject] protected LocationService LocationService { get; set; } = default!;
    [Inject] protected NavigationManager Navigation { get; set; } = default!;
    [Inject] protected IConfiguration Configuration { get; set; } = default!;
    [Inject] protected IJSRuntime JS { get; set; } = default!;
    [Inject] protected IWebHostEnvironment Env { get; set; } = default!;

    protected LocationEvent? newEvent { get; set; }
    protected List<LocationEvent> events = new();
    protected bool isAuthorized = false;
    private IBrowserFile? selectedFile;

    private DateTime GetCleanNow()
    {
        var now = DateTime.Now;
        return new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
    }

    protected override void OnInitialized()
    {
        var cleanNow = GetCleanNow();
        newEvent ??= new LocationEvent
        {
            StartTime = cleanNow,
            EndTime = cleanNow.AddHours(4)
        };

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
        if (isAuthorized)
        {
            events = await LocationService.GetEventsAsync();
        }
    }

    protected void HandleFileSelected(InputFileChangeEventArgs e)
    {
        selectedFile = e.File;
    }

    protected async Task HandleSubmit()
    {
        if (newEvent != null)
        {
            if (selectedFile != null)
            {
                var folderPath = Path.Combine(Env.WebRootPath, "images", "events");
                Directory.CreateDirectory(folderPath); 

                var fileName = $"{Guid.NewGuid()}.jpg";
                var filePath = Path.Combine(folderPath, fileName);

                var resizedImage = await selectedFile.RequestImageFileAsync("image/jpeg", 800, 600);

                await using var stream = new FileStream(filePath, FileMode.Create);
                await resizedImage.OpenReadStream(maxAllowedSize: 10485760).CopyToAsync(stream); 

                newEvent.ImagePath = $"/images/events/{fileName}";
            }

            newEvent.StartTime = new DateTime(newEvent.StartTime.Year, newEvent.StartTime.Month, newEvent.StartTime.Day, newEvent.StartTime.Hour, newEvent.StartTime.Minute, 0);
            newEvent.EndTime = new DateTime(newEvent.EndTime.Year, newEvent.EndTime.Month, newEvent.EndTime.Day, newEvent.EndTime.Hour, newEvent.EndTime.Minute, 0);

            if (newEvent.Id == 0)
                await LocationService.CreateEventAsync(newEvent);
            else
                await LocationService.UpdateEventAsync(newEvent);
        }

        events = await LocationService.GetEventsAsync();

        var cleanNow = GetCleanNow();
        newEvent = new LocationEvent { StartTime = cleanNow, EndTime = cleanNow.AddHours(4) };
        selectedFile = null;
        StateHasChanged();
    }

    protected void EditEvent(LocationEvent ev)
    {
        newEvent = ev;
        StateHasChanged();
    }

    protected async Task DeleteEvent(int id)
    {
        bool confirmed = await JS.InvokeAsync<bool>("confirm", "Är du helt säker på att du vill ta bort detta event?");

        if (confirmed)
        {
            var evToDelete = events.FirstOrDefault(e => e.Id == id);

            if (evToDelete != null && !string.IsNullOrEmpty(evToDelete.ImagePath))
            {
                var relativePath = evToDelete.ImagePath.TrimStart('/');
                var filePath = Path.Combine(Env.WebRootPath, relativePath);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            await LocationService.DeleteEventAsync(id);
            events = await LocationService.GetEventsAsync();
            StateHasChanged();
        }
    }
}