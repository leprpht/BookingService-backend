using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;
using BookingService.Housing.Services;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StayController(IStayService service) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetStay(
        int userId,
        [FromQuery] PeriodRequest periodRequest,
        [FromBody] PageRequest pageRequest)
    {
        var stays = await service.GetStays(userId, periodRequest, pageRequest);
        return Ok(stays);
    }
    
    [Authorize]
    [HttpGet("{stayId}")]
    public async Task<IActionResult> GetStayDetailsAsync(int stayId)
    {
        var stay = await service.GetStayByIdAsync(stayId);
        if (stay == null)
        {
            return NotFound();
        }
        return Ok(stay);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateStayAsync(
        [FromBody] StayCreationDto stayCreationDto)
    {
        await service.CreateAsync(stayCreationDto);
        return Created();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStayAsync(
        int id,
        [FromBody] StayUpdateDto stayUpdateDto)
    {
        if (id != stayUpdateDto.Id)
        {
            return BadRequest("Stay ID mismatch.");
        }
        
        await service.UpdateAsync(stayUpdateDto);
        return Ok();
    }
    
    [Authorize]
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatusAsync(
        int id,
        [FromBody] StayStatus status)
    {
        await service.UpdateStatusAsync(id, status);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStayAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}