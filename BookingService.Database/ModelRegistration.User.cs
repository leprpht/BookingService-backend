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
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .HasMany(e => e.Reviews)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .HasMany(e => e.Properties)
            .WithOne()
            .HasForeignKey(e => e.OwnerId)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .HasMany(e => e.Units)
            .WithOne()
            .HasForeignKey(e => e.OwnerId)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .HasIndex(e => e.Email)
            .IsUnique();
    }
}