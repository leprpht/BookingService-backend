using BookingService.Housing.DTOs;
using BookingService.Housing.Models;
using BookingService.Housing.Utils;

namespace BookingService.Housing.Services;

public interface IHousingService
{
    Task<HousingInfoDto?> GetHousingById(int id, DateOnly from, DateOnly to);
    Task<List<HousingInfoDto>> GetHousingsByFilters(FilterOptions filter, int page, int pageSize);
    Task CreateHousing(HousingCreationDto housing);
    Task UpdateHousing(HousingUpdateDto housing);
    Task DeleteHousing(int id);
    Task<HousingInfoDto?> GetHousingByStayId(int id);
}