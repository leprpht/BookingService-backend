namespace BookingService.Shared;

public sealed class PeriodRequest
{
    public required DateOnly From { get; init; }
    public required DateOnly To { get; init; }
    public int DaysCount => To.DayNumber - From.DayNumber;
}