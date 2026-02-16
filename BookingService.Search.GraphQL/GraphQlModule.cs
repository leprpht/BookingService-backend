using BookingService.Search.GraphQL.Types.Property;
using BookingService.Search.GraphQL.Types.Unit;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Search.GraphQL;

public static partial class GraphQlModule
{
    public static IServiceCollection RegisterGraphQlModule(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryTypes()
            .AddType<PropertyType>()
            .AddType<PropertyPageType>()
            .AddType<UnitListType>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        return services;
    }
}