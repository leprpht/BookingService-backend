using BookingService.Housing.DTOs;
using BookingService.Housing.Models;

namespace BookingService.Shared;

public static class UnitExtensions
{
    public static UnitListDto ToUnitListDto(this Unit unit, PeriodRequest period)
    {
        return new UnitListDto
        {
            Id = unit.Id,
            Name = unit.Name,
            Capacity = unit.Capacity,
            Price = unit.Price * period.DaysCount,
            Size = unit.Size
        };
    }
    
    public static UnitDto ToUnitDto(this Unit unit, PeriodRequest period)
    {
        return new UnitDto
        {
            Id = unit.Id,
            Name = unit.Name,
            Capacity = unit.Capacity,
            Price = unit.Price * period.DaysCount,
            Size = unit.Size
        };
    }

    public static List<UnitCustomizationDto> ToUnitCustomizationDtoList(
        this ICollection<UnitCustomization> unitCustomizationList)
    {
        return unitCustomizationList
            .GroupBy(u => u.Type)
            .Select(u => new UnitCustomizationDto
            {
                Type = u.Key.UnitCustomizationToString(),
                Text = u.Select(c => c.Text).ToList()
            })
            .ToList();
    }

    private static string UnitCustomizationToString(this CustomizationType customization)
    {
        return customization switch
        {
            CustomizationType.Parking => "Parking",
            CustomizationType.Internet => "Internet",
            CustomizationType.Breakfast => "Breakfast",
            CustomizationType.Kitchen => "Kitchen",
            CustomizationType.Bedroom => "Bedroom",
            CustomizationType.Bathroom => "Bathroom",
            CustomizationType.LivingArea => "Living Area",
            CustomizationType.Media => "Media",
            CustomizationType.Pets => "Pets",
            CustomizationType.Miscellaneous => "Miscellaneous",
            _ => "Unknown"
        };
    }
}