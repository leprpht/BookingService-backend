using BookingService.Housing;
using BookingService.Profile;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .RegisterHousingModule()
    .RegisterProfileModule();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
