using BookingService.Housing.DTOs;
using BookingService.Shared;

namespace BookingService.Housing.Services;

public interface IHousingService
{
    Task<HousingInfoDto?> GetHousingById(int id, PeriodRequest period);
    Task<List<HousingInfoDto>> GetHousingsByFilters(FilterOptions filter, PageRequest page);
    Task CreateHousing(HousingCreationDto housing);
    Task UpdateHousing(HousingUpdateDto housing);
    Task DeleteHousing(int id);
    Task<HousingInfoDto?> GetHousingByStayId(int id);
}