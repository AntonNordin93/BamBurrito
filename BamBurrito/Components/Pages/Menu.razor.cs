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
                new MenuItem { Id = 1, Name = "Asado", Price = 135, Category = "Burritos", Description = "Flankstek, Ris, Majs, Ost, Grönsaker, Guacamole, Pebre & Vitlökssås.", IsAvailable = true },
                new MenuItem { Id = 2, Name = "Pollo", Price = 125, Category = "Burritos", Description = "Kyckling, Ris, Majs, Ost, Grönsaker, Guacamole, Pebre & Vitlökssås.", IsAvailable = true },
                new MenuItem { Id = 3, Name = "Vegetariano", Price = 115, Category = "Burritos", Description = "Svarta Bönor, Ris, Majs, Ost, Grönsaker, Guacamole, Pebre & Vitlökssås.", IsAvailable = true },

                // --- KATEGORI: BOWLS (TILLGÄNGLIGA) ---
                new MenuItem { Id = 4, Name = "Asado Bowl", Price = 135, Category = "Bowls", Description = "Flankstek, Ris, Majs, Grönsaker, Guacamole, Pebre, Vitlökssås & Nachos.", IsAvailable = true },
                new MenuItem { Id = 5, Name = "Pollo Bowl", Price = 125, Category = "Bowls", Description = "Kyckling, Ris, Majs, Grönsaker, Guacamole, Pebre, Vitlökssås & Nachos.", IsAvailable = true },
                new MenuItem { Id = 6, Name = "Vegetariano Bowl", Price = 115, Category = "Bowls", Description = "Svarta Bönor, Ris, Majs, Grönsaker, Guacamole, Pebre, Vitlökssås & Nachos.", IsAvailable = true },

                // --- KATEGORI: TILLBEHÖR (TILLGÄNGLIGA) ---
                new MenuItem { Id = 7, Name = "Nachos", Price = 25, Category = "Tillbehör", Description = "Klassiska saltade nachochips, perfekta att dippa.", IsAvailable = true },
                new MenuItem { Id = 8, Name = "Guacamole", Price = 25, Category = "Tillbehör", Description = "En stor klick av vår hemvevade, färska guacamole.", IsAvailable = true },

                // --- KATEGORI: DRYCK (TILLGÄNGLIGA) ---
                new MenuItem { Id = 9, Name = "Läsk", Price = 25, Category = "Dryck", Description = "Coca-Cola, Coca-Cola Zero, Sprite, Fanta.", IsAvailable = true },
                new MenuItem { Id = 10, Name = "Jarritos", Price = 29, Category = "Dryck", Description = "Ananas, Mango, Lime, Mexican Cola, Jordgubb, Grape", IsAvailable = true }
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