using BookingService.Housing.Models;

namespace BookingService.Housing.Services;

public interface IHousingService
{
    Task<IEnumerable<HousingInfo>> GetAllHousings();
    Task<HousingInfo?> GetHousingById(int id);
    Task CreateHousing(HousingInfo housing);
    Task UpdateHousing(HousingInfo housing);
    Task DeleteHousing(int id);
}