using BookingService.Housing.Models;

namespace BookingService.Housing.Data;

public interface IHousingRepository
{
    Task<IEnumerable<HousingInfo>> GetAll();
    Task<HousingInfo?> GetById(int id);
    Task Create(HousingInfo housing);
    Task Update(HousingInfo housing);
    Task Delete(int id);
}