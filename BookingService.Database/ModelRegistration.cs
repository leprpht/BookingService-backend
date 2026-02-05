using Microsoft.EntityFrameworkCore;

namespace BookingService.Database;

public static partial class ModelRegistration
{
    public static void RegisterAll(
        this ModelBuilder modelBuilder,
        params Action<ModelBuilder>[] registrations)
    {
        foreach (var registration in registrations)
        {
            registration(modelBuilder);
        }
    }
}