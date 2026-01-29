using BookingService.Housing.Data;
using BookingService.Housing.Models;

namespace BookingService.Housing.Services;

public class HousingService(IHousingRepository repository) : IHousingService
{
    public async Task<IEnumerable<HousingInfo>> GetAllHousings()
    {
        return await repository.GetAll();
    }

    public async Task<HousingInfo?> GetHousingById(int id)
    {
        return await repository.GetById(id);
    }

    public async Task CreateHousing(HousingInfo housing)
    {
        await repository.Create(housing);
    }

    public async Task UpdateHousing(HousingInfo housing)
    {
        await repository.Update(housing);
    }

    public async Task DeleteHousing(int id)
    {
        await repository.Delete(id);
    }
}