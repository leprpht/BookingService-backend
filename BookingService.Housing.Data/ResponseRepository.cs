using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Exceptions;
using BookingService.Shared.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class ResponseRepository(BookingServiceDbContext context)
    : BaseRepository<PropertyReviewResponse>(context), IResponseRepository
{
    public async Task UpdateCommentAsync(Guid id, Guid userId, string comment)
    {
        var response = await DbSet.FirstOrDefaultAsync(r => r.Id == id);
        
        if (response == null)
            throw new NotFoundException("Review response not found.");
        
        if (response.UserId != userId)
            throw new ForbidException("User ID mismatch.");
        
        response.Comment = comment;
        await Context.SaveChangesAsync();
    }
}