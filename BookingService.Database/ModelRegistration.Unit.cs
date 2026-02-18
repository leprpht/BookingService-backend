using BookingService.Housing.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Database;

public static partial class ModelRegistration
{
    public static void RegisterUnitModels(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Unit>()
            .HasMany(e => e.Stays)
            .WithOne(e => e.Unit)
            .HasForeignKey(e => e.UnitId)
            .IsRequired();
        
        modelBuilder.Entity<Unit>()
            .HasMany(e => e.Customizations)
            .WithOne(e => e.Unit)
            .HasForeignKey(e => e.UnitId)
            .IsRequired();
        
        modelBuilder.Entity<Unit>()
            .HasMany(e => e.Pictures)
            .WithOne(e => e.Unit)
            .HasForeignKey(e => e.UnitId)
            .IsRequired();
        
        modelBuilder.Entity<Unit>()
            .HasMany(e => e.AdditionalServices)
            .WithOne(e => e.Unit)
            .HasForeignKey(e => e.UnitId)
            .IsRequired();
        
        modelBuilder.Entity<Unit>()
            .HasOne(e => e.Property)
            .WithMany(e => e.Units)
            .HasForeignKey(e => e.PropertyId)
            .IsRequired();
    }
}