using BookingService.Housing.Services;
using BookingService.Housing.Services.Subservices;
using BookingServices.Housing.Data.Subrepositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Housing;

public static partial class HousingModule
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
}