using BookingService.Housing.Data;
using BookingService.Housing.DTOs;
using BookingService.Housing.Models;
using BookingService.Housing.Utils;

namespace BookingService.Housing.Services;

public class HousingService(IHousingRepository repository) : IHousingService
{
    public async Task<HousingInfo?> GetHousingById(int id)
    {
        var housing = await repository.GetById(id);
        return housing;
    }

    public async Task<List<HousingInfoDto>> GetHousingsByFilters(FilterOptions filter, int page, int pageSize)
    {
        var housings = await repository.GetByFilters(filter, page, pageSize);
        return housings
            .ToList();
    }

    public async Task CreateHousing(HousingCreationDto housing)
    {
        await repository.Create(housing.ToModel());
    }

    public async Task UpdateHousing(HousingUpdateDto housing)
    {
        await repository.Update(housing);
    }

    public async Task DeleteHousing(int id)
    {
        await repository.Delete(id);
    }
}