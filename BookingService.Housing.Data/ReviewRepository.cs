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
    public async Task<List<(PropertyReview PropertyReview, Guest Guest)>> GetReviewsByPropertyIdAsync(
        int id,
        PageRequest pageRequest,
        ReviewFilterOptions filterOptions)
    {
        var reviews = DbSet.Where(x => x.PropertyId == id);

        reviews = filterOptions switch
        {
            ReviewFilterOptions.HighestRating => reviews.OrderByDescending(x => x.Rating),
            ReviewFilterOptions.LowestRating => reviews.OrderBy(x => x.Rating),
            ReviewFilterOptions.Newest => reviews.OrderByDescending(x => x.CreatedAt),
            ReviewFilterOptions.Oldest => reviews.OrderBy(x => x.CreatedAt),
            _ => throw new ArgumentOutOfRangeException(nameof(filterOptions))
        };

        var pagedReviews = await reviews
            .Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize)
            .Take(pageRequest.PageSize)
            .Join(Context.Guests,
                review => review.GuestId,
                guest => guest.Id,
                (review, guest) => new { review, guest })
            .ToListAsync();

        return pagedReviews.Select(x => (x.review, x.guest)).ToList();
    }


    public async Task<List<(PropertyReview PropertyReview, Guest Guest)>> GetReviewsByUserIdAsync(
        int id,
        PageRequest pageRequest,
        ReviewFilterOptions filterOptions)
    {
        var reviews = DbSet.Where(x => x.GuestId == id);

        reviews = filterOptions switch
        {
            ReviewFilterOptions.HighestRating => reviews.OrderByDescending(x => x.Rating),
            ReviewFilterOptions.LowestRating => reviews.OrderBy(x => x.Rating),
            ReviewFilterOptions.Newest => reviews.OrderByDescending(x => x.CreatedAt),
            ReviewFilterOptions.Oldest => reviews.OrderBy(x => x.CreatedAt),
            _ => throw new ArgumentOutOfRangeException(nameof(filterOptions))
        };

        var pagedReviews = await reviews
            .Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize)
            .Take(pageRequest.PageSize)
            .Join(Context.Guests,
                review => review.GuestId,
                guest => guest.Id,
                (review, guest) => new { review, guest })
            .ToListAsync();

        return pagedReviews.Select(x => (x.review, x.guest)).ToList();
    }

    public async Task<(PropertyReview PropertyReview, Guest Guest)?> GetReviewByIdAsync(int reviewId)
    {
        var result = await DbSet
            .Where(x => x.Id == reviewId)
            .Join(Context.Guests,
                review => review.GuestId,
                guest => guest.Id,
                (review, guest) => new { review, guest })
            .SingleOrDefaultAsync();

        return result == null ? null : (result.review, result.guest);
    }

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