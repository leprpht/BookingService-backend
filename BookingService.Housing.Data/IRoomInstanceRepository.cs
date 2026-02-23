using BookingService.Housing.Models;

namespace BookingServices.Housing.Data;

public interface IRoomInstanceRepository
{
    Task<RoomInstance?> GetByIdAsync(Guid id);
    Task<List<RoomInstance>> GetByUnitIdAsync(Guid unitId);
    Task<RoomInstance?> FindAvailableAsync(Guid unitId, DateOnly from, DateOnly to);
    Task AddRangeAsync(Guid unitId, Guid ownerId, List<RoomInstance> rooms);
    Task UpdateStatusAsync(Guid id, RoomStatus status);
    Task DeleteAsync(Guid id, Guid ownerId);
}