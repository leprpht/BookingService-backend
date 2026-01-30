namespace BookingService.Shared;

public sealed record PeriodRequest(
    DateOnly From,
    DateOnly To);