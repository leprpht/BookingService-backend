using BookingService.Housing.DTOs.Stay;
using BookingService.Housing.Models;
using BookingService.Housing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.Housing.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StayController(IStayService service) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a stay",
        Description = "Creates a new stay booking for a unit.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> CreateStayAsync(
        [FromBody] StayCreationDto stayCreationDto)
    {
        await service.CreateAsync(stayCreationDto);
        return Created();
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a stay",
        Description = "Updates an existing stay booking.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
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
        await service.UpdateStatusAsync(id, status);
        return Ok();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a stay",
        Description = "Deletes an existing stay booking.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> DeleteStayAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}