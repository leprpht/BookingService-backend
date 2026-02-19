using BookingService.Housing.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Database;

public static partial class ModelRegistration
{
    public static void RegisterPropertyReviewModels(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PropertyReview>()
            .HasOne(p => p.Property)
            .WithMany(p => p.Reviews)
            .HasForeignKey(p => p.PropertyId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PropertyReview>()
            .HasMany(p => p.PropertyReviewResponses)
            .WithOne(p => p.PropertyReview)
            .HasForeignKey(p => p.PropertyReviewId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<PropertyReviewResponse>()
            .HasOne(p => p.Property)
            .WithMany()
            .HasForeignKey(p => p.PropertyId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}