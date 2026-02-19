using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Exceptions;
using BookingService.Shared.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class ReviewRepository(BookingServiceDbContext context)
    : BaseRepository<PropertyReview>(context), IReviewRepository
{
    public override async Task AddAsync(PropertyReview review)
    {
        var property = await Context.Properties.FirstOrDefaultAsync(x => x.Id == review.PropertyId);
        property?.UpdateRating();
        
        await base.AddAsync(review);
    }
    
    public async Task UpdateCommentAsync(Guid id, Guid userId, string comment)
    {
        var review = await DbSet.FirstOrDefaultAsync(r => r.Id == id);
        
        if (review == null)
            throw new NotFoundException("Review not found.");
        
        if (review.UserId != userId)
            throw new ForbidException();
        
        review.Comment = comment;
        await Context.SaveChangesAsync();
    }
    
    public override async Task DeleteAsync(Guid reviewId, Guid ownerId)
    {
        var property = await Context.Properties.FirstOrDefaultAsync(x => x.Id == reviewId);
        property?.UpdateRating();
        
        await base.DeleteAsync(reviewId, ownerId);
    }
}