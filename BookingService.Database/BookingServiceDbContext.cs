using BookingService.Housing.Models;
using BookingService.Profile.Model;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Database;

public class BookingServiceDbContext(DbContextOptions<BookingServiceDbContext> options) : DbContext(options)
{
    public DbSet<Property> Properties { get; set; } = null!;
    public DbSet<PropertyPicture> PropertyPictures { get; set; } = null!;
    public DbSet<PropertyReview> PropertyReviews { get; set; } = null!;
    public DbSet<PropertyReviewResponse> PropertyReviewResponses { get; set; } = null!;
    public DbSet<Unit> Units { get; set; } = null!;
    public DbSet<UnitCustomization> UnitCustomizations { get; set; } = null!;
    public DbSet<UnitPicture> UnitPictures { get; set; } = null!;
    public DbSet<Stay> Stays { get; set; } = null!;
    public DbSet<Guest> Guests { get; set; } = null!;
    public DbSet<Owner> Owners { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.RegisterAll(ModelRegistration.RegisterPropertyModels,
            ModelRegistration.RegisterPropertyReviewModels,
            ModelRegistration.RegisterUnitModels,
            ModelRegistration.RegisterGuestModels,
            ModelRegistration.RegisterOwnerModels);
    }
}