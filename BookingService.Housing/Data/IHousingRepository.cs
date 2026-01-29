using BookingService.Housing.Models;
using BookingService.Housing.Utils;

namespace BookingService.Housing.Data;

public interface IHousingRepository
{
    Task<IEnumerable<HousingInfo>> GetAll(int page, int pageSize);
    Task<HousingInfo?> GetById(int id);
    Task<IEnumerable<HousingInfo>> GetByFilters(FilterOptions filter, int page, int pageSize);
    Task Create(HousingInfo housing);
    Task Update(HousingInfo housing);
    Task Delete(int id);
}