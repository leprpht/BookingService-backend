using BookingService.Housing.DTOs;
using BookingService.Housing.Models;

namespace BookingService.Housing.Utils;

public static class Extensions
{
    public static HousingInfo ToModel(this HousingCreationDto dto)
    {
        return new()
        {
            Name = dto.Name,
            Address = dto.Address,
            City = dto.City,
            State = dto.State,
            Country = dto.Country,
            Capacity = dto.Capacity,
            Price = dto.Price
        };
    }
}