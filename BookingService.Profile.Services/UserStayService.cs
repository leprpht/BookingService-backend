using AutoMapper;
using BookingService.Housing.DTOs.Stay;
using BookingService.Profile.Data;
using BookingService.Profile.Model;
using BookingService.Shared.Extensions;
using BookingService.Shared.Requests;

namespace BookingService.Profile.Services;

public class UserStayService(IUserStayRepository repository, IMapper mapper) : IUserStayService
{
    public async Task<List<StayDto>> GetUserStaysAsync(Guid userId, PageRequest pageRequest, StaySearchFilter filter)
    {
        var stays = await repository.GetUserStays(userId, pageRequest.PageNumber, pageRequest.PageSize, filter);
        return stays
            .Select(s => s.ToStayDto(mapper))
            .ToList();
    }
}