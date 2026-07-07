using System.ComponentModel.DataAnnotations;

namespace BamBurrito.Core.Entities;

public class LocationEvent
{
    public int Id { get; set; }
    public string GroupId { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Title { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Address { get; set; } = string.Empty;

    public string? ImagePath { get; set; }
}