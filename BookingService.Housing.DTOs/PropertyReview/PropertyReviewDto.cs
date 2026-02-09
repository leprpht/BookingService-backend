using BookingService.Housing.DTOs.Property;
using BookingService.Profile.Dtos;

namespace BookingService.Housing.DTOs.PropertyReview;

public class PropertyReviewDto
{
    public required int Id { get; init; }
    public required int Rating { get; init; }
    public string? Comment { get; init; }
    public required DateTime CreatedAt { get; init; }
    
    public required UserInfoDto User { get; set; }
    public required PropertyInfoDto Property { get; set; }
}