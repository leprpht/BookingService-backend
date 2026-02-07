using AutoMapper;
using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Models;
using BookingService.Shared.Requests;

namespace BookingService.Shared.Extensions;

public static class UnitExtensions
{
    public static UnitDto ToUnitDto(this Unit unit, PeriodRequest period, IMapper mapper)
    {
        return mapper.Map<UnitDto>(unit, opt => opt.Items["Period"] = period);
    }

    public static Unit ToUnit(this UnitCreationDto dto, IMapper mapper)
    {
        return mapper.Map<Unit>(dto);
    }

    public static Unit ToUnit(this UnitUpdateDto dto, IMapper mapper)
    {
        return mapper.Map<Unit>(dto);
    }

    public static UnitCustomization ToUnitCustomization(this UnitCustomizationCreationDto dto, int unitId, IMapper mapper)
    {
        var customization = mapper.Map<UnitCustomization>(dto);
        customization.UnitId = unitId;
        return customization;
    }

    public static UnitCustomization ToUnitCustomization(this UnitCustomizationUpdateDto dto, int unitId, IMapper mapper)
    {
        var customization = mapper.Map<UnitCustomization>(dto);
        customization.UnitId = unitId;
        return customization;
    }

    public static List<UnitCustomizationDto> ToUnitCustomizationDtoList(
        this ICollection<UnitCustomization> unitCustomizationList)
    {
        return unitCustomizationList
            .GroupBy(u => u.Type)
            .Select(u => new UnitCustomizationDto
            {
                Type = u.Key.UnitCustomizationToString(),
                Customizations = u.Select(c => new UnitCustomizationGroupedDto
                {
                    Id = c.Id,
                    Text = c.Text
                }).ToList()
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