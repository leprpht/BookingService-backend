using BookingService.Housing.Services;
using BookingServices.Housing.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Housing;

public static class HousingModule
{
    public static IServiceCollection RegisterHousingModule(this IServiceCollection services)
    {
        services
            .RegisterRepositories()
            .RegisterServices();
        
        return services;
    }
    
    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IPropertyRepository, PropertyRepository>()
            .AddScoped<IReviewRepository, ReviewRepository>()
            .AddScoped<IStayRepository, StayRepository>()
            .AddScoped<IUnitRepository, UnitRepository>();
        
        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .AddScoped<IPropertyService, PropertyService>()
            .AddScoped<IReviewService, ReviewService>()
            .AddScoped<IStayService, StayService>()
            .AddScoped<IUnitService, UnitService>();
        
        return services;
    }
}