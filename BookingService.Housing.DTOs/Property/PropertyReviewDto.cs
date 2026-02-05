using BookingService.Profile.Dtos;

namespace BookingService.Housing.DTOs.Property;

public class PropertyReviewDto
{
    public required int Id { get; init; }
    public required int Rating { get; init; }
    public string? Comment { get; init; }
    public required DateTime CreatedAt { get; init; }
    public string? Response { get; init; }
    
    public required UserInfoDto Guest { get; init; }
    public required PropertyInfoDto Property { get; init; }
}