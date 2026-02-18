using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Shared.Infrastructure.Exceptions;
using BookingService.Shared.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class ResponseRepository(BookingServiceDbContext context)
    : BaseRepository<PropertyReviewResponse>(context), IResponseRepository
{
    public async Task UpdateCommentAsync(int id, string comment)
    {
        var response = await DbSet.FirstOrDefaultAsync(r => r.Id == id);
        
        if (response == null)
            throw new NotFoundException("Review response not found.");
        
        response.Comment = comment;
        await Context.SaveChangesAsync();
    }
}