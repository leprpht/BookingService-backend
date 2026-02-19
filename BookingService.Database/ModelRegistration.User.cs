using BookingService.Profile.Model;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Database;

public static partial class ModelRegistration
{
    public static void RegisterUserModels(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Stays)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasMany(e => e.Reviews)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasMany(e => e.Properties)
            .WithOne()
            .HasForeignKey(e => e.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(e => e.Units)
            .WithOne()
            .HasForeignKey(e => e.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<User>()
            .HasIndex(e => e.Email)
            .IsUnique();
    }
}