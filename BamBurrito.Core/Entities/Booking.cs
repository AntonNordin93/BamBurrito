namespace BamBurrito.Core.Entities;

public class Booking
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;

    // Datum och tid när de vill boka foodtrucken
    public DateTime BookingDate { get; set; }

    // Vart ska foodtrucken köra och vad är det för typ av event?
    public string EventLocation { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Admin-kontroll: Är bokningen godkänd av ägaren?
    public bool IsAccepted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}