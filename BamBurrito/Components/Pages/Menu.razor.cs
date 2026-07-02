using Microsoft.AspNetCore.Components;
using BamBurrito.Core.Entities;

namespace BamBurrito.Components.Pages
{
    public partial class Menu
    {
        protected List<MenuItem> AllMenuItems { get; set; } = new();

        protected override void OnInitialized()
        {
            AllMenuItems = new List<MenuItem>
            {
                // --- KATEGORI: BURRITOS (TILLGÄNGLIGA) ---
                new MenuItem { Id = 1, Name = "Asado", Price = 139, Category = "Burritos", Description = "Flankstek, majs, ost, avocado, paprika, chimichurri och couscous.", IsAvailable = true },
                new MenuItem { Id = 2, Name = "Pollito", Price = 129, Category = "Burritos", Description = "Kyckling, majs, ost, avocado, pebre, ris och paprika.", IsAvailable = true },
                new MenuItem { Id = 3, Name = "Frijoles", Price = 115, Category = "Burritos", Description = "Svarta bönor, majs, ost, avocado, pebre, ris och paprika.", IsAvailable = true },

                // --- KATEGORI: TACOS & QUESADILLAS (KOMMANDE) ---
                new MenuItem { Id = 4, Name = "Tres Tacos", Price = 125, Category = "Tacos", Description = "Tre majstacos med valfritt protein, lök, koriander och syrad rödlök.", IsAvailable = false },
                new MenuItem { Id = 5, Name = "Quesadilla", Price = 95, Category = "Tacos", Description = "Vete-tortilla fylld med smält ost, jalapeños och valfri fyllning.", IsAvailable = false },

                // --- KATEGORI: TILLBEHÖR (KOMMANDE) ---
                new MenuItem { Id = 6, Name = "Nachos El Bam", Price = 55, Category = "Tillbehör", Description = "Krispiga majschips med smält ost, salsa och gräddfil.", IsAvailable = false },
                new MenuItem { Id = 7, Name = "Guacamole Extra", Price = 25, Category = "Tillbehör", Description = "En stor klick av vår hemvevade, färska guacamole.", IsAvailable = true },

                // --- KATEGORI: DRYCK (TILLGÄNGLIGA) ---
                new MenuItem { Id = 8, Name = "Klassisk Läsk / Loka", Price = 25, Category = "Dryck", Description = "Coca-Cola, Sprite, Fanta, Trocadero eller Loka.", IsAvailable = true },
                new MenuItem { Id = 9, Name = "Jarritos Premium", Price = 29, Category = "Dryck", Description = "Mexikansk kultläsk – Välj mellan Mango, Jordgubb eller Ananas.", IsAvailable = true }
            };
        }

        // Returnerar helt unika sicksack-banor baserat på id för asymmetriska former på prislapparna
        protected string GetPricePath(int id)
        {
            return (id % 3) switch
            {
                0 => "M 6,8 L 35,4 L 65,12 L 95,5 L 114,8 L 115,22 L 112,35 L 114,44 L 95,42 L 65,45 L 35,41 L 7,44 L 4,30 L 7,18 Z",
                1 => "M 4,5 L 40,8 L 75,3 L 100,6 L 116,4 L 113,18 L 115,32 L 112,46 L 90,43 L 60,45 L 25,42 L 5,45 L 6,25 L 4,12 Z",
                _ => "M 7,4 L 30,5 L 60,3 L 90,7 L 113,5 L 116,20 L 113,36 L 115,43 L 95,45 L 65,41 L 35,44 L 8,42 L 4,28 L 5,15 Z"
            };
        }
    }
}