using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class RoomInstanceService(IRoomInstanceRepository repository) : IRoomInstanceService
{
    public async Task<List<RoomInstanceDto>> GetByUnitIdAsync(Guid unitId)
    {
        var rooms = await repository.GetByUnitIdAsync(unitId);
        return rooms.Select(ToDto).ToList();
    }

    public async Task AddRoomsAsync(Guid unitId, Guid ownerId, List<RoomInstanceCreationDto> dtos)
    {
        var rooms = dtos.Select(d => new RoomInstance
        {
            Id = Guid.NewGuid(),
            RoomNumber = d.RoomNumber,
            Status = RoomStatus.Available
        }).ToList();

        await repository.AddRangeAsync(unitId, ownerId, rooms);
    }

    public async Task UpdateStatusAsync(Guid id, Guid ownerId, string status)
    {
        if (!Enum.TryParse<RoomStatus>(status, ignoreCase: true, out var roomStatus))
            throw new ArgumentException($"Invalid room status '{status}'.");

        await repository.UpdateStatusAsync(id, roomStatus);
    }

    public async Task DeleteAsync(Guid id, Guid ownerId)
        => await repository.DeleteAsync(id, ownerId);

    private static RoomInstanceDto ToDto(RoomInstance r) => new()
    {
        Id = r.Id,
        RoomNumber = r.RoomNumber,
        Status = r.Status.ToString(),
        UnitId = r.UnitId
    };
}