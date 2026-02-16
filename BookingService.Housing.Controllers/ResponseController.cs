using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Housing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ResponseController(IResponseService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateResponseAsync(
        [FromBody] PropertyReviewResponseCreationDto responseCreationDto)
    {
        await service.CreateAsync(responseCreationDto);
        return Created();
    }
    
    [HttpPut("{id}")]
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
    public async Task<IActionResult> UpdateResponseCommentAsync(
        int id,
        [FromBody] string comment)
    {
        await service.UpdateCommentAsync(id, comment);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResponseAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}