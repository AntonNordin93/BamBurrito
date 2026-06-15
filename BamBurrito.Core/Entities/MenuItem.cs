namespace BamBurrito.Core.Entities;

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Priset på maten
    public decimal Price { get; set; }

    // Kategori, t.ex. "Burritos", "Tacos", "Dryck", "Tillbehör"
    public string Category { get; set; } = string.Empty;

    // Är rätten aktiv/tillgänglig just nu? (Om t.ex. en ingrediens är slut)
    public bool IsAvailable { get; set; } = true;
}