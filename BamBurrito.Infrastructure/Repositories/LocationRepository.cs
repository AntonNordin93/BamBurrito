using BamBurrito.Core.Entities;
using BamBurrito.Core.Interfaces;
using BamBurrito.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BamBurrito.Infrastructure.Repositories;

public class LocationRepository(ApplicationDbContext context) : ILocationRepository
{
    public async Task<List<LocationEvent>> GetAllAsync() =>
        await context.LocationEvents.OrderBy(e => e.EventDate).ToListAsync();

    public async Task AddAsync(LocationEvent locationEvent)
    {
        context.LocationEvents.Add(locationEvent);
        await context.SaveChangesAsync();
    }
}