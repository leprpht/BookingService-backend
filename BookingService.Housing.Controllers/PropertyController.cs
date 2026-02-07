using BookingService.Housing.DTOs.Property;
using BookingService.Housing.Services;
using BookingService.Shared.Filters;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController(IPropertyService propertyService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPropertiesAsync(
        [FromQuery] HousingFilterOptions housingFilterOptions,
        [FromBody] PageRequest pageRequest)
    {
        var properties = await propertyService.GetAvailablePropertiesAsync(housingFilterOptions, pageRequest);
        return Ok(properties);
    }
    
    [HttpGet("{propertyId}")]
    public async Task<IActionResult> GetPropertyDetails(
        int propertyId,
        [FromBody] PeriodRequest periodRequest)
    {
        var property = await propertyService.GetPropertyDetailsAsync(propertyId, periodRequest);
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
        await propertyService.CreateAsync(createPropertyDto);
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

        await propertyService.UpdateAsync(propertyUpdateDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePropertyAsync(int id)
    {
        await propertyService.DeleteAsync(id);
        return NoContent();
    }
}