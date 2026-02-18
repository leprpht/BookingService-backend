using System.Security.Claims;
using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Housing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.Profile.Controllers;

[ApiController]
[Authorize]
[Route("api/User/reviews")]
public class UserReviewController(
    IReviewService reviewService,
    IResponseService responseService) : ControllerBase
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
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var userId = int.Parse(userIdClaim.Value);
        propertyReviewCreationDto.UserId = userId;
        
        await reviewService.CreateAsync(userId, propertyReviewCreationDto);
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
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var userId = int.Parse(userIdClaim.Value);
        
        if (id != propertyReviewUpdateDto.Id)
            return BadRequest("Review ID mismatch.");
        
        await reviewService.UpdateAsync(userId, propertyReviewUpdateDto);
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
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var userId = int.Parse(userIdClaim.Value);
        
        await reviewService.UpdateCommentAsync(userId, id, comment);
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
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = int.Parse(userIdClaim.Value);

        await reviewService.DeleteAsync(userId, id);
        return NoContent();
    }
    
    [HttpPost("{reviewId}/responses")]
    [SwaggerOperation(
        Summary = "Create a review response",
        Description = "Creates a new response to a property review.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> CreateResponseAsync(
        [FromBody] PropertyReviewResponseCreationDto responseCreationDto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var userId = int.Parse(userIdClaim.Value);
        responseCreationDto.UserId = userId;
        
        await responseService.CreateAsync(userId, responseCreationDto);
        return Created();
    }

    [HttpPost("{reviewId}/responses/{responseId}")]
    [SwaggerOperation(
        Summary = "Update a review response",
        Description = "Updates an existing review response.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateResponseAsync(
        int reviewId,
        int responseId,
        [FromBody] PropertyReviewResponseUpdateDto responseUpdateDto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        if (reviewId != responseUpdateDto.PropertyReviewId
            || responseId != responseUpdateDto.Id)
            return BadRequest("Response ID mismatch.");
        
        var userId = int.Parse(userIdClaim.Value);
        
        await responseService.UpdateAsync(userId, responseUpdateDto);
        return NoContent();
    }

    [HttpPatch("{reviewId}/responses/{responseId}/comment")]
    [SwaggerOperation(
        Summary = "Update response comment",
        Description = "Updates the comment text of an existing review response.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateResponseCommentAsync(
        int reviewId,
        int responseId,
        [FromBody] string comment)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var userId = int.Parse(userIdClaim.Value);
        
        await responseService.UpdateCommentAsync(responseId, userId, comment);
        return NoContent();
    }

    [HttpDelete("{reviewId}/responses/{responseId}")]
    [SwaggerOperation(
        Summary = "Delete a review response",
        Description = "Deletes an existing review response.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> DeleteResponseAsync(
        int reviewId,
        int responseId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = int.Parse(userIdClaim.Value);

        await responseService.DeleteAsync(responseId, userId);
        return NoContent();
    }
}