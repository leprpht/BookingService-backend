using System.Security.Claims;
using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;
using BookingService.Housing.Services;
using BookingService.Hubs;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.Booking.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BookingController(
    IStayService service,
    BookingHub hub) : ControllerBase
{
    [HttpPost("{propertyId}")]
    [SwaggerOperation(
        Summary = "Create a stay",
        Description = "Creates a new stay booking for a unit.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> CreateBooking(
        Guid propertyId,
        [FromBody] StayCreationDto stayCreationDto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        await service.CreateAsync(userId, stayCreationDto);
        
        await hub.Clients.Group($"property-{propertyId}")
            .SendAsync("PropertyAvailabilityUpdate", propertyId);
        
        return Created();
    }

    [HttpPatch("{propertyId}/bookings/{stayId}/status")]
    [SwaggerOperation(
        Summary = "Update stay status",
        Description = "Updates the status of an existing stay booking.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateStatusAsync(
        Guid propertyId,
        Guid stayId,
        [FromBody] StayStatus status)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        await service.UpdateStatusAsync(stayId, userId, status);
        
        await hub.Clients.Group($"property-{propertyId}")
            .SendAsync("PropertyAvailabilityUpdate", propertyId);
        
        return Ok();
    }
}