using BookingService.Search.GraphQL.Queries;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Search;

public static partial class GraphQlModule
{
    private static IRequestExecutorBuilder AddQueryTypes(this IRequestExecutorBuilder services)
    {
        services
            .AddQueryType(d => d.Name("Query"))
            .AddTypeExtension<PropertyQuery>()
            .AddTypeExtension<UnitQuery>()
            .AddTypeExtension<UnitAdditionalServicesQuery>()
            .AddTypeExtension<ReviewQuery>();

        return services;
    }
}