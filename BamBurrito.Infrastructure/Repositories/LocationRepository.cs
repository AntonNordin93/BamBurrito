using BamBurrito.Core.Entities;
using BamBurrito.Core.Interfaces;
using BamBurrito.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BamBurrito.Infrastructure.Repositories;

public class LocationRepository(ApplicationDbContext context) : ILocationRepository
{
    public async Task<List<LocationEvent>> GetAllAsync() =>
        await context.LocationEvents.OrderBy(e => e.StartTime).ToListAsync();

    public async Task AddAsync(LocationEvent locationEvent)
    {
        context.LocationEvents.Add(locationEvent);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var ev = await context.LocationEvents.FindAsync(id);
        if (ev != null) { context.LocationEvents.Remove(ev); await context.SaveChangesAsync(); }
    }

    public async Task UpdateAsync(LocationEvent locationEvent)
    {
        context.LocationEvents.Update(locationEvent);
        await context.SaveChangesAsync();
    }
}