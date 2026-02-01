using BookingService.Housing.Models;
using BookingService.Profile.Model;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Database;

public class BookingServiceDbContext(DbContextOptions<BookingServiceDbContext> options) : DbContext(options)
{
    public DbSet<Property> Properties { get; set; } = null!;
    public DbSet<PropertyPicture> PropertyPictures { get; set; } = null!;
    public DbSet<Unit> Units { get; set; } = null!;
    public DbSet<UnitCustomization> UnitCustomizations { get; set; } = null!;
    public DbSet<Stay> Stays { get; set; } = null!;
    public DbSet<Guest> Guests { get; set; } = null!;
    public DbSet<Owner> Owners { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
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
        
        modelBuilder.Entity<Guest>()
            .HasMany(e => e.Stays)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        
        modelBuilder.Entity<Owner>()
            .HasMany(e => e.Properties)
            .WithOne()
            .HasForeignKey(e => e.OwnerId)
            .IsRequired();
    }
}