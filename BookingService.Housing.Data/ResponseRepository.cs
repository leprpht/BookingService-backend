using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Profile.Model;
using BookingService.Shared.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookingServices.Housing.Data;

public class ResponseRepository(BookingServiceDbContext context)
    : BaseRepository<PropertyReviewResponse>(context), IResponseRepository
{
    public async Task UpdateCommentAsync(int id, string comment)
    {
        var response = await DbSet.SingleOrDefaultAsync(r => r.Id == id);
        if (response != null)
        {
            response.Comment = comment;
            await Context.SaveChangesAsync();
        }
    }
}