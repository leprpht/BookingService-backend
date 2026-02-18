using System.Security.Claims;
using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;
using BookingService.Housing.Services;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.Booking.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BookingController(IStayService service) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a stay",
        Description = "Creates a new stay booking for a unit.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public IActionResult CreateBooking(
        [FromQuery] int unitId,
        [FromBody] PeriodRequest period)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var userId = int.Parse(userIdClaim.Value);
        
        var stayCreationDto = new StayCreationDto
        {
            UnitId = unitId,
            From = period.From,
            To = period.To,
            Status = "Pending"
        };

        service.CreateAsync(userId, stayCreationDto);
        return Created();
    }
    
    [HttpPatch("{id}/status")]
    [SwaggerOperation(
        Summary = "Update stay status",
        Description = "Updates the status of an existing stay booking.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateStatusAsync(
        int id,
        [FromBody] StayStatus status)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var userId = int.Parse(userIdClaim.Value);
        
        await service.UpdateStatusAsync(id, userId, status);
        return Ok();
    }
}