using BookingService.Shared.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Shared;

public static class SharedModule
{
    public static IServiceCollection RegisterSharedModule(this IServiceCollection services)
    {
        services
            .AddAutoMapper(
                typeof(PropertyMappingProfile).Assembly,
                typeof(UnitMappingProfile).Assembly,
                typeof(UserMappingProfile).Assembly,
                typeof(StayMappingProfile).Assembly);

        return services;
    }
}