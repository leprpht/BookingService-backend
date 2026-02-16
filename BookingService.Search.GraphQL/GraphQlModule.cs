using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Search.GraphQL;

public static partial class GraphQlModule
{
    public static IServiceCollection RegisterGraphQlModule(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryTypes()
            .AddTypes()
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        return services;
    }
}