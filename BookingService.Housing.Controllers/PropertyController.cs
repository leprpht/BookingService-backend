using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Services;
using BookingService.Housing.Services.Subservices;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController(IPropertyService service, IPropertyPictureService pictureService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPropertiesAsync(
        [FromQuery] HousingFilterOptions housingFilterOptions,
        [FromQuery] PageRequest pageRequest)
    {
        var properties = await service.GetAvailablePropertiesAsync(housingFilterOptions, pageRequest);
        return Ok(properties);
    }
    
    [HttpGet("{propertyId}")]
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
        await service.CreateAsync(createPropertyDto);
        return Created();
    }
    
    [HttpPost("{propertyId}/pictures")]
    public async Task<IActionResult> AddPropertyPictureAsync(
        int propertyId,
        [FromBody] List<PropertyPictureCreationDto> pictureCreationDto)
    {
        await pictureService.AddRangeAsync(propertyId, pictureCreationDto);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePropertyAsync(
        int id,
        [FromBody] PropertyUpdateDto propertyUpdateDto)
    {
        if (id != propertyUpdateDto.Id)
        {
            return BadRequest("Property ID mismatch.");
        }

        await service.UpdateAsync(propertyUpdateDto);
        return Ok();
    }
    
    [HttpPut("{id}/pictures")]
    public async Task<IActionResult> UpdatePropertyPicturesAsync(
        int id,
        [FromBody] List<PropertyPictureUpdateDto> pictureUpdateDtos)
    {
        await pictureService.UpdateRangeAsync(id, pictureUpdateDtos);
        return Ok();
    }
    
    [HttpPatch("{id}/name")]
    public async Task<IActionResult> UpdatePropertyNameAsync(
        int id,
        [FromBody] string name)
    {
        await service.UpdateNameAsync(id, name);
        return Ok();
    }
    
    [HttpPatch("{id}/description")]
    public async Task<IActionResult> UpdatePropertyDescriptionAsync(
        int id,
        [FromBody] string description)
    {
        await service.UpdateDescriptionAsync(id, description);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePropertyAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpDelete("{propertyId}/pictures")]
    public async Task<IActionResult> DeletePropertyPicturesAsync(int propertyId)
    {
        await pictureService.DeleteRangeAsync(propertyId);
        return NoContent();
    }
}