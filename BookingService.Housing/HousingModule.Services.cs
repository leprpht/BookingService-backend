using BookingService.Housing.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Housing;

public static partial class HousingModule
{
    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .AddScoped<IPropertyService, PropertyService>()
            .AddScoped<IResponseService, ResponseService>()
            .AddScoped<IReviewService, ReviewService>()
            .AddScoped<IStayService, StayService>()
            .AddScoped<IUnitService, UnitService>();
        
        return services;
    }
}