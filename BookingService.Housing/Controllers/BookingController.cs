using BookingService.Housing.Services;
using BookingService.Shared;
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
    public async Task<IActionResult> GetStaysByLocationId(
        int id,
        [FromQuery] PeriodRequest period)
    {
        var stays = await service.GetStaysByLocationId(id, period);
        return Ok(stays);
    }

    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetStaysByUserId(
        int id,
        [FromQuery] PeriodRequest period)
    {
        var stays = await service.GetStaysByUserId(id, period);
        return Ok(stays);
    }
}