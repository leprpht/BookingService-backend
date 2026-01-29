using BookingService.Housing.DTOs;
using BookingService.Housing.Models;

namespace BookingService.Housing.Utils;

public static class Extensions
{
    public static HousingInfoDto ToHousingInfoDto(this HousingInfo housing)
    {
        return new()
        {
            Id = housing.Id,
            Name = housing.Name,
            Address = housing.Address,
            City = housing.City,
            State = housing.State,
            Country = housing.Country,
            Capacity = housing.Capacity
        };
    }
    
    public static HousingInfo ToModel(this HousingInfoDto housingDto)
    {
        return new(
            housingDto.Id,
            housingDto.Name,
            housingDto.Address,
            housingDto.City,
            housingDto.State,
            housingDto.Country,
            housingDto.Capacity);
    }
}