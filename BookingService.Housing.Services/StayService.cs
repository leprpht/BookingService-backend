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
    protected override Stay MapCreate(int userId, StayCreationDto dto) => dto.ToStay(userId, mapper);
    protected override Stay MapUpdate(int userId, StayUpdateDto dto) => dto.ToStay(userId, mapper);

    public override async Task CreateAsync(int userId, StayCreationDto createDto)
    {
        var stay = MapCreate(userId, createDto);
        stay.Price = stay.To.DayNumber - stay.From.DayNumber * stay.Unit.Price;
        
        await repository.AddAsync(stay);
    }

    public async Task UpdateStatusAsync(int stayId, int userId, StayStatus status)
    {
        var existingStay = await repository.GetByUserIdAsync(userId);
        
        if (existingStay == null)
            throw new NotFoundException("Stay not found.");
        
        if (existingStay.Id != stayId)
            throw new ArgumentException("Stay ID does not match the user's stay.");
        
        await repository.UpdateStatusAsync(stayId, status);
    }
}