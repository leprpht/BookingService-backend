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

    public static Unit ToUnit(this UnitCreationDto dto, Guid userId, IMapper mapper)
    {
        var unit = mapper.Map<Unit>(dto);
        unit.OwnerId = userId;
        return unit;
    }

    public static Unit ToUnit(this UnitUpdateDto dto, Guid userId, IMapper mapper)
    {
        var unit = mapper.Map<Unit>(dto);
        unit.OwnerId = userId;
        return unit;
    }

    public static UnitCustomization ToUnitCustomization(this UnitCustomizationCreationDto dto, Guid unitId,
        IMapper mapper)
    {
        var customization = mapper.Map<UnitCustomization>(dto);
        customization.UnitId = unitId;
        return customization;
    }

    public static UnitCustomization ToUnitCustomization(this UnitCustomizationUpdateDto dto, Guid unitId,
        IMapper mapper)
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

    public static UnitPicture ToUnitPicture(this UnitPictureCreationDto dto, Guid unitId, IMapper mapper)
    {
        var picture = mapper.Map<UnitPicture>(dto);
        picture.UnitId = unitId;
        return picture;
    }

    public static UnitPicture ToUnitPicture(this UnitPictureUpdateDto dto, Guid unitId, IMapper mapper)
    {
        var picture = mapper.Map<UnitPicture>(dto);
        picture.Id = unitId;
        return picture;
    }

    public static UnitAdditionalServices ToUnitAdditionalServices(this UnitAdditionalServicesCreationDto dto,
        IMapper mapper)
    {
        return mapper.Map<UnitAdditionalServices>(dto);
    }

    public static UnitAdditionalServices ToUnitAdditionalServices(this UnitAdditionalServicesUpdateDto dto,
        IMapper mapper)
    {
        return mapper.Map<UnitAdditionalServices>(dto);
    }

    public static UnitAdditionalServicesDto ToUnitAdditionalServicesDto(this UnitAdditionalServices additionalServices,
        IMapper mapper)
    {
        return mapper.Map<UnitAdditionalServicesDto>(additionalServices);
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