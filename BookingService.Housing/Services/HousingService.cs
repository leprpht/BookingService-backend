using BookingService.Housing.Data;
using BookingService.Housing.DTOs;
using BookingService.Housing.Models;
using BookingService.Housing.Utils;

namespace BookingService.Housing.Services;

public class HousingService(IHousingRepository repository) : IHousingService
{
    public async Task<HousingInfoDto?> GetHousingById(int id, DateOnly from, DateOnly to)
    {
        var housing = await repository.GetById(id, from, to);
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
    
    public async Task<HousingInfoDto?> GetHousingByStayId(int id)
    { 
        return await repository.GetHousingByStayId(id);
    }
}