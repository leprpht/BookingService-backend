using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Housing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.Housing.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReviewController(IReviewService service) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a property review",
        Description = "Creates a new review for a property.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> CreateReviewAsync(
        [FromBody] PropertyReviewCreationDto propertyReviewCreationDto)
    {
        await service.CreateAsync(propertyReviewCreationDto);
        return Created();
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a property review",
        Description = "Updates an existing property review.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
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
    [SwaggerOperation(
        Summary = "Update review comment",
        Description = "Updates the comment text of an existing property review.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateReviewCommentAsync(
        int id,
        [FromBody] string comment)
    {
        await service.UpdateCommentAsync(id, comment);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a property review",
        Description = "Deletes an existing property review.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> DeleteReviewAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}