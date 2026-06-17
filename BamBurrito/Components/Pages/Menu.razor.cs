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
                new MenuItem { Id = 1, Name = "Asado", Price = 139, Category = "Burritos", Description = "Flankstek, majs, ost, avocado, paprika, chimichurri och couscous.", IsAvailable = true },
                new MenuItem { Id = 2, Name = "Pollito", Price = 129, Category = "Burritos", Description = "Kyckling, majs, ost, avocado, pebre, ris och paprika.", IsAvailable = true },
                new MenuItem { Id = 3, Name = "Frijoles", Price = 115, Category = "Burritos", Description = "Svarta bönor, majs, ost, avocado, pebre, ris och paprika.", IsAvailable = true },

                new MenuItem { Id = 4, Name = "Klassisk Läsk / Loka", Price = 25, Category = "Dryck", Description = "Coca-Cola, Sprite, Fanta, Trocadero eller Loka.", IsAvailable = true },
                new MenuItem { Id = 5, Name = "Jarritos Premium", Price = 29, Category = "Dryck", Description = "Mexikansk kultläsk – Välj mellan Mango, Jordgubb eller Ananas.", IsAvailable = true }
            };
        }
    }
}