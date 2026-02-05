using BookingService.Profile.Model;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Database;

public static partial class ModelRegistration
{
    public static void RegisterGuestModels(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Guest>()
            .HasMany(e => e.Stays)
            .WithOne()
            .HasForeignKey(e => e.GuestId)
            .IsRequired();
        
        modelBuilder.Entity<Guest>()
            .HasMany(e => e.Reviews)
            .WithOne()
            .HasForeignKey(e => e.GuestId)
            .IsRequired();
    }
     
    public static void RegisterOwnerModels(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Owner>()
            .HasMany(e => e.Properties)
            .WithOne()
            .HasForeignKey(e => e.OwnerId)
            .IsRequired();
    }
}