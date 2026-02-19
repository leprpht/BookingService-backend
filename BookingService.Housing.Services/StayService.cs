using AutoMapper;
using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Infrastructure.Exceptions;
using BookingService.Shared.Infrastructure.Service;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class StayService(IStayRepository repository, IMapper mapper)
    : BaseService<Stay, StayCreationDto, StayUpdateDto>(repository), IStayService
{
    protected override Stay MapCreate(Guid userId, StayCreationDto dto) => dto.ToStay(userId, mapper);
    protected override Stay MapUpdate(Guid userId, StayUpdateDto dto) => dto.ToStay(userId, mapper);

    public override async Task CreateAsync(Guid userId, StayCreationDto createDto)
    {
        var stay = MapCreate(userId, createDto);
        stay.Price = stay.To.DayNumber - stay.From.DayNumber * stay.Unit.Price;
        
        await repository.AddAsync(stay);
    }

    public async Task UpdateStatusAsync(Guid stayId, Guid userId, StayStatus newStatus)
    {
        var existingStay = await repository.GetByIdAsync(stayId);
        
        if (existingStay == null)
            throw new NotFoundException("Stay not found.");
        
        var isGuest = existingStay.UserId == userId;
        var isHost = existingStay.Unit.OwnerId == userId;

        if (!isGuest && !isHost)
            throw new ForbidException("You are not authorized to update this stay.");

        var currentStatus = existingStay.Status;

        if (currentStatus is StayStatus.Cancelled or StayStatus.Completed)
            throw new ForbidException("Cannot modify a finished stay.");

        switch (newStatus)
        {
            case StayStatus.Confirmed:
                if (!isHost || currentStatus is not StayStatus.Pending)
                    throw new ForbidException("Only host can confirm a pending stay.");
                break;

            case StayStatus.Cancelled:
                if (currentStatus is not (StayStatus.Pending or StayStatus.Confirmed))
                    throw new ForbidException("Cannot cancel this stay.");
                break;

            case StayStatus.Completed:
                if (!isHost || currentStatus is not StayStatus.Confirmed)
                    throw new ForbidException("Only host can complete a confirmed stay.");
                break;

            case StayStatus.Pending:
                throw new ForbidException("Cannot revert status to pending.");

            default:
                throw new ForbidException("Invalid status transition.");
        }

        await repository.UpdateStatusAsync(stayId, newStatus);
    }
}