using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Services;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/stays")]
public class StayController(IStayService stayService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetStay(
        int guestId,
        [FromQuery] PeriodRequest periodRequest,
        [FromBody] PageRequest pageRequest)
    {
        var stays = await stayService.GetStays(guestId, periodRequest, pageRequest);
        return Ok(stays);
    }
    
    [HttpGet("{stayId}")]
    public async Task<IActionResult> GetStayDetailsAsync(int stayId)
    {
        var stay = await stayService.GetStayByIdAsync(stayId);
        if (stay == null)
        {
            return NotFound();
        }
        return Ok(stay);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStayAsync(
        [FromBody] StayCreationDto stayCreationDto)
    {
        await stayService.CreateAsync(stayCreationDto);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStayAsync(
        int id,
        [FromBody] StayUpdateDto stayUpdateDto)
    {
        if (id != stayUpdateDto.Id)
        {
            return BadRequest("Stay ID mismatch.");
        }
        
        await stayService.UpdateAsync(stayUpdateDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStayAsync(int id)
    {
        await stayService.DeleteAsync(id);
        return NoContent();
    }
}