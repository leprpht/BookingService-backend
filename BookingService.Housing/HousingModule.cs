using BookingService.Housing.Services;
using BookingService.Housing.Services.Subservices;
using BookingServices.Housing.Data;
using BookingServices.Housing.Data.Subrepositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Housing;

public static class HousingModule
{
    public static IServiceCollection RegisterHousingModule(this IServiceCollection services)
    {
        services
            .RegisterRepositories()
            .RegisterSubrepositories()
            .RegisterServices()
            .RegisterSubservices();
        
        return services;
    }
    
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
    
    private static IServiceCollection RegisterSubrepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IPropertyPictureRepository, IPropertyPictureRepository>()
            .AddScoped<IUnitCustomizationRepository, UnitCustomizationRepository>()
            .AddScoped<IUnitPictureRepository, UnitPictureRepository>();
        
        return services;
    }

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

    private static IServiceCollection RegisterSubservices(this IServiceCollection services)
    {
        services
            .AddScoped<IPropertyService, PropertyService>()
            .AddScoped<IUnitCustomizationService, UnitCustomizationService>()
            .AddScoped<IUnitPictureService, UnitPictureService>();
        
        return services;
    }
}