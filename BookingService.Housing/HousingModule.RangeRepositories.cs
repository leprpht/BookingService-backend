using BookingServices.Housing.Data.RangeRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Housing;

public static partial class HousingModule
{
    private static IServiceCollection RegisterRangeRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IPropertyPictureRepository, PropertyPictureRepository>()
            .AddScoped<IUnitCustomizationRepository, UnitCustomizationRepository>()
            .AddScoped<IUnitPictureRepository, UnitPictureRepository>();
        
        return services;
    }
}