namespace BamBurrito.Core.Entities;

public class LocationEvent
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}