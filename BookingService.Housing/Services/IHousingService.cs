using BookingService.Housing.DTOs;
using BookingService.Housing.Utils;

namespace BookingService.Housing.Services;

public interface IHousingService
{
    Task<List<HousingInfoDto>> GetAllHousings(int page, int pageSize);
    Task<HousingInfoDto?> GetHousingById(int id);
    Task<List<HousingInfoDto>> GetHousingsByFilters(FilterOptions filter, int page, int pageSize);
    Task CreateHousing(HousingInfoDto housing);
    Task UpdateHousing(HousingInfoDto housing);
    Task DeleteHousing(int id);
}