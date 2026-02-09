using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Housing.Services;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController(IReviewService reviewService) : ControllerBase
{
    [HttpGet("property/{propertyId}")]
    public async Task<IActionResult> GetReviewsByPropertyIdAsync(
        int propertyId,
        [FromQuery] ReviewFilterOptions filterOptions,
        [FromQuery] PageRequest pageRequest)
    {
        var reviews = await reviewService.GetReviewsByPropertyIdAsync(propertyId, filterOptions, pageRequest);
        return Ok(reviews);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetReviewsByUserIdAsync(
        int userId,
        [FromQuery] ReviewFilterOptions filterOptions,
        [FromQuery] PageRequest pageRequest)
    {
        var reviews = await reviewService.GetReviewsByUserIdAsync(userId, filterOptions, pageRequest);
        return Ok(reviews);
    }

    [HttpGet("{reviewId}")]
    public async Task<IActionResult> GetReviewById(int reviewId)
    {
        var review = await reviewService.GetReviewById(reviewId);
        if (review == null)
        {
            return NotFound();
        }
        return Ok(review);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReviewAsync(
        [FromBody] PropertyReviewCreationDto propertyReviewCreationDto)
    {
        await reviewService.CreateAsync(propertyReviewCreationDto);
        return Created();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReviewAsync(int id)
    {
        await reviewService.DeleteAsync(id);
        return NoContent();
    }
}