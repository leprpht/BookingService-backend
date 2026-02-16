using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Housing.Services;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReviewController(IReviewService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateReviewAsync(
        [FromBody] PropertyReviewCreationDto propertyReviewCreationDto)
    {
        await service.CreateAsync(propertyReviewCreationDto);
        return Created();
    }

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

    [HttpPatch("{id}/comment")]
    public async Task<IActionResult> UpdateReviewCommentAsync(
        int id,
        [FromBody] string comment)
    {
        await service.UpdateCommentAsync(id, comment);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReviewAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}