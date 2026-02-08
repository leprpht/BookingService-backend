using System.Text.Json.Serialization;
using BookingService.Database;
using BookingService.Housing;
using BookingService.Profile;
using BookingService.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<BookingServiceDbContext>(options =>
    options.UseSqlServer(
        connectionString,
        m => m.MigrationsAssembly("BookingService.Database")));

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app
    .UseHttpsRedirection()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
