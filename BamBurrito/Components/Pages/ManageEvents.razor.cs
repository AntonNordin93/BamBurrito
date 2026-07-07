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

    // Proxy properties för UI
    protected DateTime MainEventDate { get; set; }
    protected int MainStartHour { get; set; }
    protected int MainStartMinute { get; set; }
    protected int MainEndHour { get; set; }
    protected int MainEndMinute { get; set; }

    public class DatePeriod
    {
        public DateTime Date { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
    }

    protected List<DatePeriod> additionalDates = new();

    protected void AddDate()
    {
        var cleanNow = GetCleanNow();
        var endDate = cleanNow.AddHours(4);
        additionalDates.Add(new DatePeriod
        {
            Date = cleanNow.Date,
            StartHour = cleanNow.Hour,
            StartMinute = cleanNow.Minute,
            EndHour = endDate.Hour,
            EndMinute = endDate.Minute
        });
    }

    protected void RemoveDate(DatePeriod dp)
    {
        additionalDates.Remove(dp);
    }

    private DateTime GetCleanNow()
    {
        var now = DateTime.Now;
        int minute = (now.Minute / 5) * 5;
        return new DateTime(now.Year, now.Month, now.Day, now.Hour, minute, 0);
    }

    protected override void OnInitialized()
    {
        var cleanNow = GetCleanNow();

        // Initiera Proxy Variablerna
        MainEventDate = cleanNow.Date;
        MainStartHour = cleanNow.Hour;
        MainStartMinute = cleanNow.Minute;
        var endInitial = cleanNow.AddHours(4);
        MainEndHour = endInitial.Hour;
        MainEndMinute = endInitial.Minute;

        newEvent ??= new LocationEvent
        {
            StartTime = cleanNow,
            EndTime = endInitial,
            GroupId = Guid.NewGuid().ToString()
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

            if (newEvent.Id == 0)
            {
                string batchGroupId = Guid.NewGuid().ToString();
                newEvent.GroupId = batchGroupId;

                // Sammanfoga Datum och Tid för Huvudeventet
                newEvent.StartTime = new DateTime(MainEventDate.Year, MainEventDate.Month, MainEventDate.Day, MainStartHour, MainStartMinute, 0);
                newEvent.EndTime = new DateTime(MainEventDate.Year, MainEventDate.Month, MainEventDate.Day, MainEndHour, MainEndMinute, 0);

                await LocationService.CreateEventAsync(newEvent);

                // Sammanfoga Datum och Tid för Extra Dagar
                foreach (var dp in additionalDates)
                {
                    var ev = new LocationEvent
                    {
                        Title = newEvent.Title,
                        Address = newEvent.Address,
                        Description = newEvent.Description,
                        ImagePath = newEvent.ImagePath,
                        StartTime = new DateTime(dp.Date.Year, dp.Date.Month, dp.Date.Day, dp.StartHour, dp.StartMinute, 0),
                        EndTime = new DateTime(dp.Date.Year, dp.Date.Month, dp.Date.Day, dp.EndHour, dp.EndMinute, 0),
                        GroupId = batchGroupId
                    };
                    await LocationService.CreateEventAsync(ev);
                }
            }
            else
            {
                // Uppdatering - Använd proxy variablerna
                newEvent.StartTime = new DateTime(MainEventDate.Year, MainEventDate.Month, MainEventDate.Day, MainStartHour, MainStartMinute, 0);
                newEvent.EndTime = new DateTime(MainEventDate.Year, MainEventDate.Month, MainEventDate.Day, MainEndHour, MainEndMinute, 0);
                await LocationService.UpdateEventAsync(newEvent);
            }
        }

        events = await LocationService.GetEventsAsync();

        // Återställ allt
        var cleanNow = GetCleanNow();
        var nextEnd = cleanNow.AddHours(4);
        newEvent = new LocationEvent { StartTime = cleanNow, EndTime = nextEnd, GroupId = Guid.NewGuid().ToString() };
        MainEventDate = cleanNow.Date;
        MainStartHour = cleanNow.Hour;
        MainStartMinute = cleanNow.Minute;
        MainEndHour = nextEnd.Hour;
        MainEndMinute = nextEnd.Minute;
        additionalDates.Clear();
        selectedFile = null;
        StateHasChanged();
    }

    protected void EditEvent(LocationEvent ev)
    {
        newEvent = ev;

        // Fyll i Proxy Variablerna från det klickade eventet
        MainEventDate = ev.StartTime.Date;
        MainStartHour = ev.StartTime.Hour;
        MainStartMinute = (ev.StartTime.Minute / 5) * 5;
        MainEndHour = ev.EndTime.Hour;
        MainEndMinute = (ev.EndTime.Minute / 5) * 5;

        additionalDates.Clear();
        StateHasChanged();
    }

    protected async Task DeleteEvent(int id)
    {
        bool confirmed = await JS.InvokeAsync<bool>("confirm", "Är du helt säker på att du vill ta bort detta event?");

        if (confirmed)
        {
            var evToDelete = events.FirstOrDefault(e => e.Id == id);

            if (evToDelete != null)
            {
                await LocationService.DeleteEventAsync(id);
                events = await LocationService.GetEventsAsync();

                if (!string.IsNullOrEmpty(evToDelete.ImagePath))
                {
                    bool isImageStillInUse = events.Any(e => e.ImagePath == evToDelete.ImagePath);

                    if (!isImageStillInUse)
                    {
                        var relativePath = evToDelete.ImagePath.TrimStart('/');
                        var filePath = Path.Combine(Env.WebRootPath, relativePath);

                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                }
            }
            StateHasChanged();
        }
    }
}