using BookingService.Housing.Models;
using BookingService.Profile.Model;
using BookingService.Shared.Repository;

namespace BookingServices.Housing.Data;

public interface IResponseRepository : IBaseRepository<PropertyReviewResponse>
{
    Task UpdateCommentAsync(int id, string comment);
}