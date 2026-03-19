using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Location;

public static class LocationModule
{
    public static IServiceCollection RegisterLocationModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<GeoNamesOptions>(
            configuration.GetSection(GeoNamesOptions.SectionName));

        services.AddHttpClient<IGeoNamesService, GeoNamesService>(client =>
        {
            client.Timeout = TimeSpan.FromSeconds(5);
        });

        services.AddScoped<ILocationNormalizationService, LocationNormalizationService>();

        return services;
    }
}