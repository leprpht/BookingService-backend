using BookingService.Housing.DTOs;
using BookingService.Housing.Models;

namespace BookingService.Shared;

public static class PropertyExtensions
{
    public static PropertyListingDto ToPropertyListingDto(this Property property, PeriodRequest period)
    {
        return new PropertyListingDto
        {
            Id = property.Id,
            Name = property.Name,
            Address = property.Address,
            City = property.City,
            State = property.State,
            Country = property.Country,
            Description = property.Description,
            Pictures = property.Pictures
                .Select(p => p.Url)
                .ToList(),
            Units = property.Units
                .Select(u => u.ToUnitListDto(period))
                .ToList()
        };
    }

    public static PropertyPageDto ToPropertyPageDto(this Property property, PeriodRequest period)
    {
        return new PropertyPageDto
        {
            Id = property.Id,
            Name = property.Name,
            Address = property.Address,
            City = property.City,
            State = property.State,
            Country = property.Country,
            Price = property.Units
                .Min(u => u.Price) * period.DaysCount,
            PictureUrl = property.Pictures
                .Where(u => u.IsCover)
                .Select(p => p.Url)
                .FirstOrDefault()
        };
    }
}