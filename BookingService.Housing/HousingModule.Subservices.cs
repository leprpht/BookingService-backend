using BookingService.Housing.Services.Subservices;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Housing;

public static partial class HousingModule
{
    private static IServiceCollection RegisterSubservices(this IServiceCollection services)
    {
        services
            .AddScoped<IPropertyPictureService, PropertyPictureService>()
            .AddScoped<IUnitCustomizationService, UnitCustomizationService>()
            .AddScoped<IUnitPictureService, UnitPictureService>();
        
        return services;
    }
}