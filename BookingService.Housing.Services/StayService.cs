using AutoMapper;
using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Requests;
using BookingService.Shared.Service;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class StayService(IStayRepository repository, IMapper mapper)
    : BaseService<Stay, StayCreationDto, StayUpdateDto>(repository), IStayService
{
    protected override Stay MapCreate(StayCreationDto dto) => dto.ToStay(mapper);
    protected override Stay MapUpdate(StayUpdateDto dto) => dto.ToStay(mapper);
    
    public async Task UpdateStatusAsync(int stayId, StayStatus status) =>
        await repository.UpdateStatusAsync(stayId, status);
}