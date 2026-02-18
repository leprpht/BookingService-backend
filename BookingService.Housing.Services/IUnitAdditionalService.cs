using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Service;

namespace BookingService.Housing.Services;

public interface IUnitAdditionalService : IBaseService<UnitAdditionalServices, UnitAdditionalServicesCreationDto, UnitAdditionalServicesUpdateDto>;