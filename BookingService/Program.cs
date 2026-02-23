using System.Text.Json.Serialization;
using BookingService.Auth;
using BookingService.Database;
using BookingService.Housing;
using BookingService.Profile;
using BookingService.Search;
using BookingService.Shared;
using BookingService.Shared.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .RegisterDatabaseModule(connectionString!)
    .RegisterSharedModule()
    .RegisterHousingModule()
    .RegisterProfileModule()
    .RegisterAuthModule(builder.Configuration)
    .RegisterGraphQlModule();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations());

var app = builder.Build();

app
    .UseMiddleware<ControllerExceptionHandler>()
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();
app.MapGraphQL();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
