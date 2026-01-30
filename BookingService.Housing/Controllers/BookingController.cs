using BookingService.Housing.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/booking")]
public class BookingController(IBookingService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStayById(int id)
    {
        var stay = await service.GetStayById(id);
        if (stay is null)
            return NotFound();
        return Ok(stay);
    }

    [HttpGet("location/{id}")]
    public async Task<IActionResult> GetStaysByLocationId(int id)
    {
        var stays = await service.GetStaysByLocationId(id);
        return Ok(stays);
    }
}