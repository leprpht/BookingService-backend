using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Services;
using BookingService.Housing.Services.RangeServices;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.Housing.Controllers;

[ApiController]
[Authorize(Roles = "Admin,Owner")]
[Route("api/[controller]")]
public class PropertyController(
    IPropertyService service,
    IPropertyPictureService pictureService,
    IAuthorizationService authorizationService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new property",
        Description = "Creates a new property with the provided data."
        )]
    [SwaggerResponse(201)]
    [SwaggerResponse(403)]
    public async Task<IActionResult> CreatePropertyAsync(
        [FromBody] PropertyCreationDto createPropertyDto)
    {
        if (!await IsAuthorizedForOwnerAsync(createPropertyDto.OwnerId))
            return Forbid();
        
        await service.CreateAsync(createPropertyDto);
        return Created();
    }
    
    [HttpPost("{propertyId}/pictures")]
    [SwaggerOperation(
        Summary = "Add pictures to a property",
        Description = "Adds one or more pictures to the specified property.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(403)]
    public async Task<IActionResult> AddPropertyPictureAsync(
        int propertyId,
        [FromQuery] int ownerId,
        [FromBody] List<PropertyPictureCreationDto> pictureCreationDto)
    {
        if (!await IsAuthorizedForOwnerAsync(ownerId))
            return Forbid();
        
        await pictureService.AddRangeAsync(propertyId, pictureCreationDto);
        return Created();
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a property",
        Description = "Updates the details of an existing property.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(403)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdatePropertyAsync(
        [FromQuery] int id,
        [FromBody] PropertyUpdateDto propertyUpdateDto)
    {
        if (!await IsAuthorizedForOwnerAsync(propertyUpdateDto.OwnerId))
            return Forbid();
        
        if (id != propertyUpdateDto.Id)
            return BadRequest("Property ID mismatch.");

        await service.UpdateAsync(propertyUpdateDto);
        return Ok();
    }

    [HttpPut("{id}/pictures")]
    [SwaggerOperation(
        Summary = "Update property pictures",
        Description = "Updates the pictures of an existing property.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(403)]
    public async Task<IActionResult> UpdatePropertyPicturesAsync(
        int id,
        [FromQuery] int ownerId,
        [FromBody] List<PropertyPictureUpdateDto> pictureUpdateDtos)
    {
        if (!await IsAuthorizedForOwnerAsync(ownerId))
            return Forbid();
        
        await pictureService.UpdateRangeAsync(id, pictureUpdateDtos);
        return Ok();
    }

    [HttpPut("{id}/tags")]
    [SwaggerOperation(
        Summary = "Update property tags",
        Description = "Updates the tags associated with an existing property.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(403)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdatePropertyTagsAsync(
        int id,
        [FromQuery] int ownerId,
        [FromBody] List<int> tagIds)
    {
        if (!await IsAuthorizedForOwnerAsync(ownerId))
            return Forbid();
        
        await service.UpdateTagsAsync(id, tagIds);
        return Ok();
    }

    [HttpPatch("{id}/name")]
    [SwaggerOperation(
        Summary = "Update property name",
        Description = "Updates the name of an existing property.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(403)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdatePropertyNameAsync(
        int id,
        [FromQuery] int ownerId,
        [FromBody] string name)
    {
        if (!await IsAuthorizedForOwnerAsync(ownerId))
            return Forbid();
        
        await service.UpdateNameAsync(id, name);
        return Ok();
    }

    [HttpPatch("{id}/description")]
    [SwaggerOperation(
        Summary = "Update property description",
        Description = "Updates the description of an existing property.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(403)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdatePropertyDescriptionAsync(
        int id,
        [FromQuery] int ownerId,
        [FromBody] string description)
    {
        if (!await IsAuthorizedForOwnerAsync(ownerId))
            return Forbid();
        
        await service.UpdateDescriptionAsync(id, description);
        return Ok();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a property",
        Description = "Deletes an existing property and all its related data.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(403)]
    public async Task<IActionResult> DeletePropertyAsync(
        int id,
        [FromQuery] int ownerId)
    {
        if (!await IsAuthorizedForOwnerAsync(ownerId))
            return Forbid();
        
        await service.DeleteAsync(id);
        return NoContent();
    }

    [HttpDelete("{propertyId}/pictures")]
    [SwaggerOperation(
        Summary = "Delete property pictures",
        Description = "Deletes all pictures associated with a property.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(403)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> DeletePropertyPicturesAsync(
        int propertyId,
        [FromQuery] int ownerId)
    {
        if (!await IsAuthorizedForOwnerAsync(ownerId))
            return Forbid();
        
        await pictureService.DeleteRangeAsync(propertyId);
        return NoContent();
    }
    
    private async Task<bool> IsAuthorizedForOwnerAsync(int id)
    {
        var authResult = await authorizationService.AuthorizeAsync(User, id, "IdMatch");
        return authResult.Succeeded;
    }
}