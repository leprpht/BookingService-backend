using BookingService.Housing.DTOs.Unit;

namespace BookingService.Housing.Services;

public interface IRoomInstanceService
{
    Task<List<RoomInstanceDto>> GetByUnitIdAsync(Guid unitId);
    Task AddRoomsAsync(Guid unitId, Guid ownerId, List<RoomInstanceCreationDto> dtos);
    Task UpdateStatusAsync(Guid id, Guid ownerId, string status);
    Task DeleteAsync(Guid id, Guid ownerId);
}