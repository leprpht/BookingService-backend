using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Housing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.Housing.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ResponseController(IResponseService service) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a review response",
        Description = "Creates a new response to a property review.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> CreateResponseAsync(
        [FromBody] PropertyReviewResponseCreationDto responseCreationDto)
    {
        await service.CreateAsync(responseCreationDto);
        return Created();
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a review response",
        Description = "Updates an existing review response.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateResponseAsync(
        int id,
        [FromBody] PropertyReviewResponseUpdateDto responseUpdateDto)
    {
        if (id != responseUpdateDto.Id)
        {
            return BadRequest("Response ID mismatch.");
        }
        
        await service.UpdateAsync(responseUpdateDto);
        return NoContent();
    }

    [HttpPatch("{id}/comment")]
    [SwaggerOperation(
        Summary = "Update response comment",
        Description = "Updates the comment text of an existing review response.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateResponseCommentAsync(
        int id,
        [FromBody] string comment)
    {
        await service.UpdateCommentAsync(id, comment);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a review response",
        Description = "Deletes an existing review response.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> DeleteResponseAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}