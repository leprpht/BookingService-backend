using BookingService.Housing.DTOs;
using BookingService.Housing.Models;
using BookingService.Housing.Utils;
using BookingService.Shared;

namespace BookingService.Housing.Data;

public interface IHousingRepository
{
    Task<HousingInfoDto?> GetById(int id, PeriodRequest period);
    Task<IEnumerable<HousingInfoDto>> GetByFilters(FilterOptions filter, PageRequest page);
    Task Create(HousingInfo housing);
    Task Update(HousingUpdateDto housing);
    Task Delete(int id);
    Task<HousingInfoDto?> GetHousingByStayId(int id);
}