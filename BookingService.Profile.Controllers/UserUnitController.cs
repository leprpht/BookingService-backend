using System.Security.Claims;
using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Services;
using BookingService.Housing.Services.RangeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.Profile.Controllers;

[ApiController]
[Authorize(Roles = "Admin,Host")]
[Route("api/User/properties")]
public class UserUnitController(
    IUnitService service,
    IUnitCustomizationService customizationService,
    IUnitPictureService pictureService,
    IUnitAdditionalService additionalService,
    IRoomInstanceService roomService)
    : ControllerBase
{
    [HttpPost("{propertyId}/units")]
    [SwaggerOperation(
        Summary = "Create a unit",
        Description = "Creates a new unit within a property.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> CreateUnitAsync(
        Guid propertyId,
        [FromBody] UnitCreationDto createUnitDto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        if (createUnitDto.PropertyId != propertyId)
            return BadRequest("Property ID in URL does not match Property ID in body.");

        var userId = Guid.Parse(userIdClaim.Value);

        await service.CreateAsync(userId, createUnitDto);
        return Created();
    }

    [HttpPost("{propertyId}/units/{unitId}/customizations")]
    [SwaggerOperation(
        Summary = "Add unit customizations",
        Description = "Adds customizations (amenities, features) to a unit.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> AddUnitCustomizationsAsync(
        Guid propertyId,
        Guid unitId,
        [FromBody] List<UnitCustomizationCreationDto> creationList)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var unit = await service.GetByIdAsync(unitId);
        if (unit == null || unit.PropertyId != propertyId)
            return BadRequest("Unit does not exist or does not belong to the specified property.");

        var userId = Guid.Parse(userIdClaim.Value);

        await customizationService.AddRangeAsync(unitId, userId, creationList);
        return Created();
    }

    [HttpPost("{propertyId}/units/{unitId}/pictures")]
    [SwaggerOperation(
        Summary = "Add unit pictures",
        Description = "Adds pictures to a unit.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> AddUnitPicturesAsync(
        Guid propertyId,
        Guid unitId,
        [FromBody] List<UnitPictureCreationDto> creationList)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var unit = await service.GetByIdAsync(unitId);
        if (unit == null || unit.PropertyId != propertyId)
            return BadRequest("Unit does not exist or does not belong to the specified property.");

        var userId = Guid.Parse(userIdClaim.Value);

        await pictureService.AddRangeAsync(unitId, userId, creationList);
        return Created();
    }

    [HttpPost("{propertyId}/units/{unitId}/services")]
    [SwaggerOperation(
        Summary = "Add unit additional services",
        Description = "Adds additional services to a unit.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> AddUnitAdditionalServicesAsync(
        Guid propertyId,
        Guid unitId,
        [FromBody] UnitAdditionalServicesCreationDto creationDto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var unit = await service.GetByIdAsync(unitId);
        if (unit == null || unit.PropertyId != propertyId)
            return BadRequest("Unit does not exist or does not belong to the specified property.");

        var userId = Guid.Parse(userIdClaim.Value);

        await additionalService.CreateAsync(userId, creationDto);
        return Created();
    }

    [HttpPut("{propertyId}/units/{unitId}")]
    [SwaggerOperation(
        Summary = "Update a unit",
        Description = "Updates an existing unit's details.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateUnitAsync(
        Guid propertyId,
        Guid unitId,
        [FromBody] UnitUpdateDto unitUpdateDto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        if (unitUpdateDto.Id != unitId || unitUpdateDto.PropertyId != propertyId)
            return BadRequest("Unit ID or Property ID in URL does not match those in body.");

        var userId = Guid.Parse(userIdClaim.Value);

        await service.UpdateAsync(userId, unitUpdateDto);
        return Ok();
    }

    [HttpPut("{propertyId}/units/{unitId}/customizations")]
    [SwaggerOperation(
        Summary = "Update unit customizations",
        Description = "Updates the customizations of an existing unit.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateUnitCustomizationsAsync(
        Guid propertyId,
        Guid unitId,
        [FromBody] List<UnitCustomizationUpdateDto> updateList)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var unit = await service.GetByIdAsync(unitId);
        if (unit == null || unit.PropertyId != propertyId)
            return BadRequest("Unit does not exist or does not belong to the specified property.");

        var userId = Guid.Parse(userIdClaim.Value);

        await customizationService.UpdateRangeAsync(userId, unitId, updateList);
        return Ok();
    }

    [HttpPut("{propertyId}/units/{unitId}/pictures")]
    [SwaggerOperation(
        Summary = "Update unit pictures",
        Description = "Updates the pictures of an existing unit.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateUnitPicturesAsync(
        Guid propertyId,
        Guid unitId,
        [FromBody] List<UnitPictureUpdateDto> updateList)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var unit = await service.GetByIdAsync(unitId);
        if (unit == null || unit.PropertyId != propertyId)
            return BadRequest("Unit does not exist or does not belong to the specified property.");

        var userId = Guid.Parse(userIdClaim.Value);

        await pictureService.UpdateRangeAsync(userId, unitId, updateList);
        return Ok();
    }

    [HttpPatch("{propertyId}/units/active")]
    [SwaggerOperation(
        Summary = "Activation toggle for all units in a property",
        Description = "Toggles the active status of all units within a property.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> ToggleAllUnitsActiveStatusAsync(
        Guid propertyId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        await service.ToggleAllUnitsActiveStatusAsync(propertyId, userId);
        return Ok();
    }

    [HttpPatch("{propertyId}/units/{unitId}/active")]
    [SwaggerOperation(
        Summary = "Activation toggle for a unit",
        Description = "Toggles the active status of an existing unit.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> ToggleUnitActiveStatusAsync(
        Guid propertyId,
        Guid unitId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var unit = await service.GetByIdAsync(unitId);
        if (unit == null || unit.PropertyId != propertyId)
            return BadRequest("Unit does not exist or does not belong to the specified property.");

        var userId = Guid.Parse(userIdClaim.Value);

        await service.ToggleActiveStatusAsync(unitId, userId);
        return Ok();
    }

    [HttpPatch("{propertyId}/units/{unitId}/name")]
    [SwaggerOperation(
        Summary = "Update unit name",
        Description = "Updates the name of an existing unit.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateUnitNameAsync(
        Guid propertyId,
        Guid unitId,
        [FromBody] string name)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var unit = await service.GetByIdAsync(unitId);
        if (unit == null || unit.PropertyId != propertyId)
            return BadRequest("Unit does not exist or does not belong to the specified property.");

        var userId = Guid.Parse(userIdClaim.Value);

        await service.UpdateNameAsync(userId, unitId, name);
        return Ok();
    }

    [HttpPatch("{propertyId}/units/{unitId}/services")]
    [SwaggerOperation(
        Summary = "Update unit additional services",
        Description = "Updates additional services for an existing unit.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateUnitAdditionalServicesAsync(
        Guid propertyId,
        Guid unitId,
        [FromBody] UnitAdditionalServicesUpdateDto updateDto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var unit = await service.GetByIdAsync(unitId);
        if (unit == null || unit.PropertyId != propertyId)
            return BadRequest("Unit does not exist or does not belong to the specified property.");

        var userId = Guid.Parse(userIdClaim.Value);

        await additionalService.UpdateAsync(userId, updateDto);
        return Ok();
    }

    [HttpDelete("{propertyId}/units/{unitId}")]
    [SwaggerOperation(
        Summary = "Delete a unit",
        Description = "Deletes an existing unit.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> DeleteUnitAsync(
        Guid propertyId,
        Guid unitId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var unit = await service.GetByIdAsync(unitId);
        if (unit == null || unit.PropertyId != propertyId)
            return BadRequest("Unit does not exist or does not belong to the specified property.");

        var userId = Guid.Parse(userIdClaim.Value);

        await service.DeleteAsync(userId, unitId);
        return NoContent();
    }

    [HttpDelete("{propertyId}/units/{unitId}/customizations")]
    [SwaggerOperation(
        Summary = "Delete unit customizations",
        Description = "Deletes all customizations for a unit.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> DeleteUnitCustomizationsAsync(
        Guid propertyId,
        Guid unitId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var unit = await service.GetByIdAsync(unitId);
        if (unit == null || unit.PropertyId != propertyId)
            return BadRequest("Unit does not exist or does not belong to the specified property.");

        var userId = Guid.Parse(userIdClaim.Value);

        await customizationService.DeleteRangeAsync(userId, unitId);
        return NoContent();
    }

    [HttpGet("{propertyId}/units/{unitId}/rooms")]
    [SwaggerOperation(
        Summary = "List rooms",
        Description = "Lists all physical room instances for a unit type.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> GetRoomsAsync(Guid propertyId, Guid unitId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var unit = await service.GetByIdAsync(unitId);
        if (unit == null || unit.PropertyId != propertyId)
            return BadRequest("Unit does not exist or does not belong to the specified property.");

        var rooms = await roomService.GetByUnitIdAsync(unitId);
        return Ok(rooms);
    }

    [HttpPost("{propertyId}/units/{unitId}/rooms")]
    [SwaggerOperation(
        Summary = "Add rooms",
        Description = "Adds one or more physical room instances to a unit type.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> AddRoomsAsync(
        Guid propertyId,
        Guid unitId,
        [FromBody] List<RoomInstanceCreationDto> dtos)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var unit = await service.GetByIdAsync(unitId);
        if (unit == null || unit.PropertyId != propertyId)
            return BadRequest("Unit does not exist or does not belong to the specified property.");

        var userId = Guid.Parse(userIdClaim.Value);
        await roomService.AddRoomsAsync(unitId, userId, dtos);
        return Created();
    }

    [HttpPatch("{propertyId}/units/{unitId}/rooms/{roomId}/status")]
    [SwaggerOperation(
        Summary = "Update room status",
        Description = "Sets a room's status (Available / Maintenance / OutOfService).")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateRoomStatusAsync(
        Guid propertyId,
        Guid unitId,
        Guid roomId,
        [FromBody] string status)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var unit = await service.GetByIdAsync(unitId);
        if (unit == null || unit.PropertyId != propertyId)
            return BadRequest("Unit does not exist or does not belong to the specified property.");

        var userId = Guid.Parse(userIdClaim.Value);

        await roomService.UpdateStatusAsync(roomId, userId, status);
        return Ok();
    }

    [HttpDelete("{propertyId}/units/{unitId}/rooms/{roomId}")]
    [SwaggerOperation(
        Summary = "Delete a room",
        Description = "Removes a physical room instance (only if it has no active stays).")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> DeleteRoomAsync(Guid propertyId, Guid unitId, Guid roomId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var unit = await service.GetByIdAsync(unitId);
        if (unit == null || unit.PropertyId != propertyId)
            return BadRequest("Unit does not exist or does not belong to the specified property.");

        var userId = Guid.Parse(userIdClaim.Value);

        await roomService.DeleteAsync(roomId, userId);
        return NoContent();
    }
}