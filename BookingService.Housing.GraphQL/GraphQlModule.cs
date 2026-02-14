using BookingService.Housing.GraphQL.Types.Property;
using BookingService.Housing.GraphQL.Types.Unit;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Housing.GraphQL;

public static class GraphQlModule
{
    public static IServiceCollection RegisterGraphQlModule(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<PropertyQuery>()
            .AddType<PropertyType>()
            .AddType<PropertyPageType>()
            .AddType<UnitListType>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        return services;
    }
}