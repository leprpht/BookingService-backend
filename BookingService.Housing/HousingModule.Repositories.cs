using BookingServices.Housing.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Housing;

public static partial class HousingModule
{
    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IPropertyRepository, PropertyRepository>()
            .AddScoped<IResponseRepository, ResponseRepository>()
            .AddScoped<IReviewRepository, ReviewRepository>()
            .AddScoped<IStayRepository, StayRepository>()
            .AddScoped<IUnitRepository, UnitRepository>();
        
        return services;
    }
}