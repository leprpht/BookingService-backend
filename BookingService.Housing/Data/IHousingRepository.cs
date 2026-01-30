using BookingService.Housing.DTOs;
using BookingService.Housing.Models;
using BookingService.Housing.Utils;

namespace BookingService.Housing.Data;

public interface IHousingRepository
{
    Task<HousingInfoDto?> GetById(int id, DateOnly from, DateOnly to);
    Task<IEnumerable<HousingInfoDto>> GetByFilters(FilterOptions filter, int page, int pageSize);
    Task Create(HousingInfo housing);
    Task Update(HousingUpdateDto housing);
    Task Delete(int id);
    Task<HousingInfoDto?> GetHousingByStayId(int id);
}