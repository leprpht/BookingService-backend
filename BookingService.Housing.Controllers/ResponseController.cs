using BookingService.Housing.DTOs.PropertyReview;
using BookingService.Housing.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResponseController(IResponseService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetResponseById(int responseId)
    {
        var response = await service.GetPropertyReviewByIdAsync(responseId);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response);
    }

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
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResponseAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}