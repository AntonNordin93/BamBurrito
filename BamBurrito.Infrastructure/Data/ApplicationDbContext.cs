using BamBurrito.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BamBurrito.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<LocationEvent> LocationEvents => Set<LocationEvent>();
    public DbSet<OfferRequest> OfferRequests => Set<OfferRequest>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Här konfigurerar vi att Price alltid ska ha 18 siffror totalt, varav 2 är decimaler
        builder.Entity<MenuItem>()
            .Property(m => m.Price)
            .HasPrecision(18, 2);
    }
}