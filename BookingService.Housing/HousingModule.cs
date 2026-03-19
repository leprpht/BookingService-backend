using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Housing;

public static partial class HousingModule
{
    public static IServiceCollection RegisterHousingModule(this IServiceCollection services)
    {
        services
            .RegisterRepositories()
            .RegisterRangeRepositories()
            .RegisterServices()
            .RegisterRangeServices();

        return services;
    }
}