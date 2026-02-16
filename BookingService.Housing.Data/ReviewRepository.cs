using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Profile.Model;
using BookingService.Shared.Filters;
using BookingService.Shared.Repository;
using BookingService.Shared.Requests;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class ReviewRepository(BookingServiceDbContext context)
    : BaseRepository<PropertyReview>(context), IReviewRepository
{
    public override async Task AddAsync(PropertyReview review)
    {
        var property = await Context.Properties.SingleOrDefaultAsync(x => x.Id == review.PropertyId);
        property?.UpdateRating();
        
        await base.AddAsync(review);
    }
    
    public async Task UpdateCommentAsync(int id, string comment)
    {
        var review = await DbSet.SingleOrDefaultAsync(r => r.Id == id);
        if (review != null)
        {
            review.Comment = comment;
            await Context.SaveChangesAsync();
        }
    }
    
    public override async Task DeleteAsync(int reviewId)
    {
        var property = await Context.Properties.SingleOrDefaultAsync(x => x.Id == reviewId);
        property?.UpdateRating();
        
        await base.DeleteAsync(reviewId);
    }
}