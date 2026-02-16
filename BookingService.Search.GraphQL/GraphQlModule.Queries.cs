using BookingService.Search.GraphQL.Queries;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Search.GraphQL;

public static partial class GraphQlModule
{
    private static IRequestExecutorBuilder AddQueryTypes(this IRequestExecutorBuilder services)
    {
        services
            .AddQueryType<PropertyQuery>();
        
        return services;
    }
}