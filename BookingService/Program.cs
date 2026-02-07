using System.Text.Json.Serialization;
using BookingService.Housing;
using BookingService.Profile;
using BookingService.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .RegisterSharedModule()
    .RegisterHousingModule()
    .RegisterProfileModule();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

app
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();
