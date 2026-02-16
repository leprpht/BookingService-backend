using AutoMapper;
using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Extensions;
using BookingService.Shared.Infrastructure.Service;
using BookingServices.Housing.Data;

namespace BookingService.Housing.Services;

public class UnitAdditionalService(IUnitAdditionalServicesRepository repository, IMapper mapper)
    : BaseService<UnitAdditionalServices, UnitAdditionalServicesCreationDto, UnitAdditionalServicesUpdateDto>(repository), IUnitAdditionalService
{
    protected override UnitAdditionalServices MapCreate(UnitAdditionalServicesCreationDto dto) => dto.ToUnitAdditionalServices(mapper);
    protected override UnitAdditionalServices MapUpdate(UnitAdditionalServicesUpdateDto dto) => dto.ToUnitAdditionalServices(mapper);
}