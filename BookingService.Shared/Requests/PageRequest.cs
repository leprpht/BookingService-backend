namespace BookingService.Shared.Requests;

public sealed class PageRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}