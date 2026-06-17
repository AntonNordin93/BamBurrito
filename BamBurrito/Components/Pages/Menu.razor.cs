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
    }
}