using BookingService.Housing.Data;
using BookingService.Housing.DTOs;
using BookingService.Housing.Utils;

namespace BookingService.Housing.Services;

public class HousingService(IHousingRepository repository) : IHousingService
{
    public async Task<List<HousingInfoDto>> GetAllHousings(int page, int pageSize)
    {
        var housing = await repository.GetAll(page, pageSize);
        return housing
            .Select(h => h.ToHousingInfoDto())
            .ToList();
    }

    public async Task<HousingInfoDto?> GetHousingById(int id)
    {
        var housing = await repository.GetById(id);
        return housing?.ToHousingInfoDto();
    }

    public async Task<List<HousingInfoDto>> GetHousingsByFilters(FilterOptions filter, int page, int pageSize)
    {
        var housings = await repository.GetByFilters(filter, page, pageSize);
        return housings
            .Select(h => h.ToHousingInfoDto())
            .ToList();
    }

    public async Task CreateHousing(HousingInfoDto housing)
    {
        await repository.Create(housing.ToModel());
    }

    public async Task UpdateHousing(HousingInfoDto housing)
    {
        await repository.Update(housing.ToModel());
    }

    public async Task DeleteHousing(int id)
    {
        await repository.Delete(id);
    }
}