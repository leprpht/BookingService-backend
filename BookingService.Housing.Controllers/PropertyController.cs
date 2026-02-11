using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Services;
using BookingService.Housing.Services.Subservices;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Authorize(Roles = "Admin,Owner")]
[Route("api/[controller]")]
public class PropertyController(
    IPropertyService service,
    IPropertyPictureService pictureService,
    IAuthorizationService authorizationService) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetPropertiesAsync(
        [FromQuery] HousingFilterOptions housingFilterOptions,
        [FromQuery] PageRequest pageRequest)
    {
        var properties = await service.GetAvailablePropertiesAsync(housingFilterOptions, pageRequest);
        return Ok(properties);
    }
    
    [HttpGet("{propertyId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPropertyDetails(
        int propertyId,
        [FromQuery] PeriodRequest periodRequest)
    {
        var property = await service.GetPropertyDetailsAsync(propertyId, periodRequest);
        if (property == null)
        {
            return NotFound();
        }
        return Ok(property);
    }

    
    [HttpPost]
    public async Task<IActionResult> CreatePropertyAsync(
        [FromBody] PropertyCreationDto createPropertyDto)
    {
        if (!await IsAuthorizedForOwnerAsync(createPropertyDto.OwnerId))
            return Forbid();
        
        await service.CreateAsync(createPropertyDto);
        return Created();
    }
    
    
    [HttpPost("{propertyId}/pictures")]
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
    public async Task<IActionResult> DeletePropertyPicturesAsync(
        int propertyId,
        [FromQuery] int ownerId)
    {
        if (!await IsAuthorizedForOwnerAsync(ownerId))
            return Forbid();
        
        await pictureService.DeleteRangeAsync(propertyId);
        return NoContent();
    }
    
    private async Task<bool> IsAuthorizedForOwnerAsync(int ownerId)
    {
        var authResult = await authorizationService.AuthorizeAsync(User, ownerId, "IdMatch");
        return authResult.Succeeded;
    }
}