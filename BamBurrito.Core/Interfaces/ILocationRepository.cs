using BamBurrito.Core.Entities;

namespace BamBurrito.Core.Interfaces;

public interface ILocationRepository
{
    Task<List<LocationEvent>> GetAllAsync();
    Task AddAsync(LocationEvent locationEvent);
}