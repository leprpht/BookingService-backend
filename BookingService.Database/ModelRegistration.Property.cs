using BookingService.Housing.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Database;

public static partial class ModelRegistration
{
    public static void RegisterPropertyModels(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Property>()
            .HasMany(e => e.Units)
            .WithOne(e => e.Property)
            .HasForeignKey(e => e.PropertyId)
            .IsRequired();

        modelBuilder.Entity<Property>()
            .HasMany(e => e.Pictures)
            .WithOne(e => e.Property)
            .HasForeignKey(e => e.PropertyId)
            .IsRequired();

        modelBuilder.Entity<Property>()
            .HasMany(e => e.Reviews)
            .WithOne(e => e.Property)
            .HasForeignKey(e => e.PropertyId)
            .IsRequired();

        modelBuilder.Entity<Property>()
            .HasMany(e => e.Tags)
            .WithMany(e => e.Properties)
            .UsingEntity(typeBuilder => typeBuilder.ToTable("PropertyTags"));

        modelBuilder.Entity<Tag>()
            .HasIndex(t => t.Text)
            .IsUnique();
    }
}