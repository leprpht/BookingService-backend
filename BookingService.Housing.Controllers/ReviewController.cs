using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Housing.Services;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController(IReviewService service) : ControllerBase
{
    [HttpGet("property/{propertyId}")]
    public async Task<IActionResult> GetReviewsByPropertyIdAsync(
        int propertyId,
        [FromQuery] ReviewFilterOptions filterOptions,
        [FromQuery] PageRequest pageRequest)
    {
        var reviews = await service.GetReviewsByPropertyIdAsync(propertyId, filterOptions, pageRequest);
        return Ok(reviews);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetReviewsByUserIdAsync(
        int userId,
        [FromQuery] ReviewFilterOptions filterOptions,
        [FromQuery] PageRequest pageRequest)
    {
        var reviews = await service.GetReviewsByUserIdAsync(userId, filterOptions, pageRequest);
        return Ok(reviews);
    }

    [HttpGet("{reviewId}")]
    public async Task<IActionResult> GetReviewById(int reviewId)
    {
        var review = await service.GetReviewById(reviewId);
        if (review == null)
        {
            return NotFound();
        }
        return Ok(review);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateReviewAsync(
        [FromBody] PropertyReviewCreationDto propertyReviewCreationDto)
    {
        await service.CreateAsync(propertyReviewCreationDto);
        return Created();
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReviewAsync(
        int id,
        [FromBody] PropertyReviewUpdateDto propertyReviewUpdateDto)
    {
        if (id != propertyReviewUpdateDto.Id)
        {
            return BadRequest("Review ID mismatch.");
        }
        
        await service.UpdateAsync(propertyReviewUpdateDto);
        return NoContent();
    }

    [Authorize]
    [HttpPatch("{id}/comment")]
    public async Task<IActionResult> UpdateReviewCommentAsync(
        int id,
        [FromBody] string comment)
    {
        await service.UpdateCommentAsync(id, comment);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReviewAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}