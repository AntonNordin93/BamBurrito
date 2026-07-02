using BamBurrito.Core.Entities;
using BamBurrito.Core.Interfaces;

namespace BamBurrito.Core.Services;

public class LocationService(ILocationRepository repo)
{
    public async Task<List<LocationEvent>> GetEventsAsync() => await repo.GetAllAsync();
    public async Task CreateEventAsync(LocationEvent newEvent) => await repo.AddAsync(newEvent);
    public async Task DeleteEventAsync(int id) => await repo.DeleteAsync(id);
    public async Task UpdateEventAsync(LocationEvent ev) => await repo.UpdateAsync(ev);
}