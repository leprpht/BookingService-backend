using AutoMapper;
using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Infrastructure.Exceptions;
using BookingService.Shared.Infrastructure.Service;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class StayService(
    IStayRepository repository,
    IRoomInstanceRepository roomRepository,
    IMapper mapper)
    : BaseService<Stay, StayCreationDto, StayUpdateDto>(repository), IStayService
{
    protected override Stay MapCreate(Guid userId, StayCreationDto dto) => dto.ToStay(userId, mapper);
    protected override Stay MapUpdate(Guid userId, StayUpdateDto dto) => dto.ToStay(userId, mapper);
    
    public override async Task CreateAsync(Guid userId, StayCreationDto dto)
    {
        var room = await roomRepository.FindAvailableAsync(dto.UnitId, dto.From, dto.To)
            ?? throw new InvalidOperationException(
                "No rooms are available for the requested unit and period.");

        var stay = MapCreate(userId, dto);
        stay.RoomInstanceId = room.Id;
        
        var additionalCosts = room.Unit.AdditionalServices
            .Where(s => dto.AdditionalServiceIds != null
                        && dto.AdditionalServiceIds.Contains(s.Id))
            .Select(s => s.Price)
            .Sum();
        
        stay.TotalPrice = room.Unit.Price * (dto.To.DayNumber - dto.From.DayNumber) + additionalCosts ?? 0;

        await repository.AddAsync(stay);
    }

    public async Task UpdateStatusAsync(Guid stayId, Guid userId, StayStatus newStatus)
    {
        var existingStay = await repository.GetByIdAsync(stayId);

        var isGuest = existingStay.UserId == userId;
        var isHost = existingStay.RoomInstance.Unit.OwnerId == userId;

        if (!isGuest && !isHost)
            throw new ForbidException("You are not authorized to update this stay.");

        var currentStatus = existingStay.Status;

        if (currentStatus is StayStatus.Cancelled or StayStatus.Completed)
            throw new ForbidException("Cannot modify a finished stay.");

        switch (newStatus)
        {
            case StayStatus.Confirmed:
                if (!isHost || currentStatus is not StayStatus.Pending)
                    throw new ForbidException("Only the host can confirm a pending stay.");
                break;

            case StayStatus.Cancelled:
                if (currentStatus is not (StayStatus.Pending or StayStatus.Confirmed))
                    throw new ForbidException("Cannot cancel a stay in its current status.");
                break;

            case StayStatus.Completed:
                if (!isHost || currentStatus is not StayStatus.Confirmed)
                    throw new ForbidException("Only the host can complete a confirmed stay.");
                break;

            case StayStatus.Pending:
                throw new ForbidException("Cannot revert status to Pending.");

            default:
                throw new ForbidException("Invalid status transition.");
        }

        await repository.UpdateStatusAsync(stayId, newStatus);
    }
}