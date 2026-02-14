using System.Text.Json.Serialization;
using BookingService.Auth;
using BookingService.Database;
using BookingService.Housing;
using BookingService.Housing.GraphQL;
using BookingService.Shared;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .RegisterDatabaseModule(connectionString!)
    .RegisterSharedModule()
    .RegisterHousingModule()
    .RegisterAuthModule(builder.Configuration)
    .RegisterGraphQlModule();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app
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
