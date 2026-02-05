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
            .WithOne()
            .HasForeignKey(e => e.UnitId)
            .IsRequired();
        
        modelBuilder.Entity<Unit>()
            .HasMany(e => e.Pictures)
            .WithOne()
            .HasForeignKey(e => e.UnitId)
            .IsRequired();
    }
}