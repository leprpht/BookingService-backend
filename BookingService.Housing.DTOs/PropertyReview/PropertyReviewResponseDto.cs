using BookingService.Housing.DTOs.Property;
using BookingService.Profile.Dtos;

namespace BookingService.Housing.DTOs.PropertyReview;

public sealed class PropertyReviewResponseDto
{
    public required int Id { get; init; }
    public required string Comment { get; init; }
    public required DateTime CreatedAt { get; init; }
    public int PropertyReviewId { get; init; }
    
    public required UserInfoDto User { get; set; }
    public required PropertyInfoDto Property { get; set; }
}