using Microsoft.AspNetCore.Components;
using BamBurrito.Core.Entities;
using BamBurrito.Core.Services;
using System.Globalization;

namespace BamBurrito.Components.Pages;

public partial class Places
{
    [Inject] protected LocationService LocationService { get; set; } = default!;

    protected List<LocationEvent> allEvents = new();

    protected int currentYear = DateTime.Now.Year;
    protected int currentMonth = DateTime.Now.Month;

    // Håller koll på vilken specifik dag användaren har valt
    protected DateTime? selectedDate = null;

    protected override async Task OnInitializedAsync()
    {
        allEvents = await LocationService.GetEventsAsync();

        // Smidig detalj: Om det finns ett event IDAG, öppna det direkt!
        var todayEvents = GetEventsForDay(DateTime.Now.Day);
        if (todayEvents.Any())
        {
            selectedDate = DateTime.Now.Date;
        }
    }

    protected void NextMonth()
    {
        if (currentMonth == 12)
        {
            currentMonth = 1;
            currentYear++;
        }
        else
        {
            currentMonth++;
        }
        selectedDate = null; // Stäng panelen när man bläddrar månad
    }

    protected void PreviousMonth()
    {
        if (currentMonth == 1)
        {
            currentMonth = 12;
            currentYear--;
        }
        else
        {
            currentMonth--;
        }
        selectedDate = null; // Stäng panelen när man bläddrar månad
    }

    // Uppdaterar den valda dagen när man klickar på en ruta i kalendern
    protected void SelectDate(int day)
    {
        selectedDate = new DateTime(currentYear, currentMonth, day);
    }

    protected IEnumerable<LocationEvent> GetEventsForDay(int day)
    {
        return allEvents.Where(e => e.StartTime.Year == currentYear &&
                                    e.StartTime.Month == currentMonth &&
                                    e.StartTime.Day == day)
                        .OrderBy(e => e.StartTime);
    }

    protected string MonthName(int month)
    {
        return new DateTime(2020, month, 1).ToString("MMMM", new CultureInfo("sv-SE"));
    }
}