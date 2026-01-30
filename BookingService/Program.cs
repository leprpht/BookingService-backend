using BookingService.Data;
using BookingService.Housing.Data;
using BookingService.Housing.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IHousingService, HousingService>();
builder.Services.AddScoped<IBookingService, BookingService.Housing.Services.BookingService>();
builder.Services.AddScoped<IHousingRepository, HousingRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddControllers();

var app = builder.Build();

app.Run();
