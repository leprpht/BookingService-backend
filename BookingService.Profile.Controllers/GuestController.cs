using BookingService.Profile.Dtos;
using BookingService.Profile.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Profile.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuestController(IGuestService guestService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGuestInfoAsync(int id)
    {
        var guest = await guestService.GetGuestInfoAsync(id);
        if (guest == null)
        {
            return NotFound();
        }
        return Ok(guest);
    }
    
    [HttpGet("{id}/profile")]
    public async Task<IActionResult> GetGuestByIdAsync(int id)
    {
        var guest = await guestService.GetGuestByIdAsync(id);
        if (guest == null)
        {
            return NotFound();
        }
        return Ok(guest);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGuestAsync(UserCreationDto guest)
    {
        await guestService.CreateGuestAsync(guest);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGuestAsync(
        int id,
        [FromBody] UserUpdateDto guestUpdateDto)
    {
        if (id != guestUpdateDto.Id)
        {
            return BadRequest("Guest ID mismatch.");
        }

        await guestService.UpdateGuestAsync(guestUpdateDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGuestAsync(int id)
    {
        await guestService.DeleteGuestAsync(id);
        return NoContent();
    }
}