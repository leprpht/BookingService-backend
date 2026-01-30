namespace BookingService.Shared;

public sealed record PageRequest(
    int PageNumber,
    int PageSize);