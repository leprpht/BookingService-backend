using System.Security.Claims;
using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Services;
using BookingService.Housing.Services.RangeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.Profile.Controllers;

[ApiController]
[Authorize(Roles = "Admin,Host")]
[Route("api/User/properties")]
public class UserPropertyController(
    IPropertyService service,
    IPropertyPictureService pictureService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new property",
        Description = "Creates a new property with the provided data."
    )]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(403)]
    public async Task<IActionResult> CreatePropertyAsync(
        [FromBody] PropertyCreationDto createPropertyDto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        await service.CreateAsync(userId, createPropertyDto);
        return Created();
    }

    [HttpPost("{propertyId}/pictures")]
    [SwaggerOperation(
        Summary = "Add pictures to a property",
        Description = "Adds one or more pictures to the specified property.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(403)]
    public async Task<IActionResult> AddPropertyPictureAsync(
        Guid propertyId,
        [FromBody] List<PropertyPictureCreationDto> pictureCreationDto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        await pictureService.AddRangeAsync(propertyId, userId, pictureCreationDto);
        return Created();
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a property",
        Description = "Updates the details of an existing property.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(403)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdatePropertyAsync(
        [FromBody] PropertyUpdateDto propertyUpdateDto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        await service.UpdateAsync(userId, propertyUpdateDto);
        return Ok();
    }

    [HttpPut("{id}/pictures")]
    [SwaggerOperation(
        Summary = "Update property pictures",
        Description = "Updates the pictures of an existing property.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(403)]
    public async Task<IActionResult> UpdatePropertyPicturesAsync(
        Guid id,
        [FromBody] List<PropertyPictureUpdateDto> pictureUpdateDtos)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        await pictureService.UpdateRangeAsync(id, userId, pictureUpdateDtos);
        return Ok();
    }

    [HttpPut("{id}/tags")]
    [SwaggerOperation(
        Summary = "Update property tags",
        Description = "Updates the tags associated with an existing property.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(403)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdatePropertyTagsAsync(
        Guid id,
        [FromBody] List<Guid> tagIds)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        await service.UpdateTagsAsync(id, userId, tagIds);
        return Ok();
    }

    [HttpPatch("{id}/name")]
    [SwaggerOperation(
        Summary = "Update property name",
        Description = "Updates the name of an existing property.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(403)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdatePropertyNameAsync(
        Guid id,
        [FromBody] string name)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        await service.UpdateNameAsync(id, userId, name);
        return Ok();
    }

    [HttpPatch("{id}/description")]
    [SwaggerOperation(
        Summary = "Update property description",
        Description = "Updates the description of an existing property.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(403)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdatePropertyDescriptionAsync(
        Guid id,
        [FromBody] string description)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        await service.UpdateDescriptionAsync(id, userId, description);
        return Ok();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a property",
        Description = "Deletes an existing property and all its related data.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(401)]
    [SwaggerResponse(403)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> DeletePropertyAsync(
        Guid id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        await service.DeleteAsync(id, userId);
        return NoContent();
    }

    [HttpDelete("{propertyId}/pictures")]
    [SwaggerOperation(
        Summary = "Delete property pictures",
        Description = "Deletes all pictures associated with a property.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(403)]
    public async Task<IActionResult> DeletePropertyPicturesAsync(
        Guid propertyId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        await pictureService.DeleteRangeAsync(propertyId, userId);
        return NoContent();
    }
}