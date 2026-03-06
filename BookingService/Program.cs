using System.Text.Json.Serialization;
using BookingService.Auth;
using BookingService.Database;
using BookingService.Housing;
using BookingService.Location;
using BookingService.Profile;
using BookingService.Search;
using BookingService.Shared;
using BookingService.Shared.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .RegisterDatabaseModule(connectionString!)
    .RegisterSharedModule()
    .RegisterLocationModule(builder.Configuration)
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

await app.MigrateDatabaseAsync();

var shouldSeedDatabase = builder.Configuration.GetValue("Database:SeedOnStartup", false);
if (shouldSeedDatabase)
{
    await app.SeedDatabaseAsync();
}

app
    .UseMiddleware<ControllerExceptionHandler>()
    .UseHttpsRedirection()
    .UseCors("Angular")
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