using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Requests;

namespace BookingService.Shared.Extensions;

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
            Size = unit.Size,
            IsAvailable = unit.Stays
                .Any(s => s.Status != StayStatus.Cancelled
                          && s.From < period.To
                          && s.To > period.From)
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

    public static Unit ToUnit(this UnitCreationDto unitCreationDto)
    {
        return new Unit
        {
            Name = unitCreationDto.Name,
            Capacity = unitCreationDto.Capacity,
            Price = unitCreationDto.Price,
            Size = unitCreationDto.Size,
            PropertyId = unitCreationDto.PropertyId
        };
    }

    public static Unit ToUnit(this UnitUpdateDto unitUpdateDto)
    {
        return new Unit
        {
            Id = unitUpdateDto.Id,
            Name = unitUpdateDto.Name,
            Capacity = unitUpdateDto.Capacity,
            Price = unitUpdateDto.Price,
            Size = unitUpdateDto.Size,
            PropertyId = unitUpdateDto.PropertyId
        };
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