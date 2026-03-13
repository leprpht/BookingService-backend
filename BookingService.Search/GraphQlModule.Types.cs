using BookingService.Search.GraphQL.Types.Property;
using BookingService.Search.GraphQL.Types.Unit;
using BookingService.Shared.Requests;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Search;

public static partial class GraphQlModule
{
    private static IRequestExecutorBuilder AddTypes(this IRequestExecutorBuilder services)
    {
        services
            .AddType<PropertyType>()
            .AddType<PeriodRequest>()
            .AddType<PropertyPageType>()
            .AddType<UnitAdditionalServicesType>()
            .AddType<UnitCustomizationType>()
            .AddType<UnitListType>()
            .AddType<UnitType>();
        
        return services;
    }
}