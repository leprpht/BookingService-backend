using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Service;

namespace BookingService.Housing.Services;

public interface IUnitService : IBaseService<Unit, UnitCreationDto, UnitUpdateDto>
{
    Task UpdateNameAsync(int id, int userId, string name);
}