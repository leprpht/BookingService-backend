using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Profile.Model;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class ReviewRepository(BookingServiceDbContext context) : IReviewRepository
{
    public async Task<List<(PropertyReview PropertyReview, Guest Guest)>> GetReviewsByPropertyIdAsync(
        int id,
        PageRequest pageRequest,
        ReviewFilterOptions filterOptions)
    {
        var reviews = context.PropertyReviews.Where(x => x.PropertyId == id);

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
            .Join(context.Guests,
                review => review.GuestId,
                guest => guest.Id,
                (review, guest) => new { review, guest })
            .ToListAsync();

        return pagedReviews.Select(x => (x.review, x.guest)).ToList();
    }


    public async Task<List<(PropertyReview PropertyReview, Guest Guest)>> GetReviewsByUserIdAsync(int id, PageRequest pageRequest, ReviewFilterOptions filterOptions)
    {
        var reviews = context.PropertyReviews.Where(x => x.GuestId == id);

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
            .Join(context.Guests,
                review => review.GuestId,
                guest => guest.Id,
                (review, guest) => new { review, guest })
            .ToListAsync();

        return pagedReviews.Select(x => (x.review, x.guest)).ToList();
    }

    public async Task<(PropertyReview PropertyReview, Guest Guest)?> GetReviewByIdAsync(int reviewId)
    {
        var result = await context.PropertyReviews
            .Where(x => x.Id == reviewId)
            .Join(context.Guests,
                review => review.GuestId,
                guest => guest.Id,
                (review, guest) => new { review, guest })
            .SingleOrDefaultAsync();

        return result == null ? null : (result.review, result.guest);
    }

    public async Task CreateReviewAsync(PropertyReview review)
    {
        await context.PropertyReviews.AddAsync(review);
        var property = await context.Properties.Where(x => x.Id == review.PropertyId).SingleOrDefaultAsync();
        property?.UpdateRating();
        
        await context.SaveChangesAsync();
    }

    public async Task AddReviewResponseAsync(int reviewId, string response)
    {
        var review = await context.PropertyReviews.SingleOrDefaultAsync(x => x.Id == reviewId);
        if (review == null)
        {
            return;
        }
        
        review.Response = response;
        await context.SaveChangesAsync();
    }

    public async Task DeleteReviewAsync(int reviewId)
    {
        var review = await context.PropertyReviews.FindAsync(reviewId);
        if (review != null)
        {
            context.PropertyReviews.Remove(review);
            var property = await context.Properties.Where(x => x.Id == review.PropertyId).SingleOrDefaultAsync();
            property?.UpdateRating();
            
            await context.SaveChangesAsync();
        }
    }
}