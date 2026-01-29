using BookingService.Housing.DTOs;
using BookingService.Housing.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/housing")]
public class HousingController(IHousingService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetHousings(
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        var housings = await service.GetAllHousings(page, pageSize);
        return Ok(housings);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHousing(int id)
    {
        var housing = await service.GetHousingById(id);
        if (housing is null)
            return NotFound();
        return Ok(housing);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetHousingsByFilters(
        [FromQuery] string? name,
        [FromQuery] string? city,
        [FromQuery] string? country,
        [FromQuery] bool? availableOnly,
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        var filter = new Utils.FilterOptions
        {
            Name = name,
            City = city,
            Country = country,
            AvailableOnly = availableOnly
        };
        var housings = await service.GetHousingsByFilters(filter, page, pageSize);
        return Ok(housings);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHousing([FromBody] HousingInfoDto housing)
    {
        await service.CreateHousing(housing);
        return CreatedAtAction(nameof(GetHousing), new { id = housing.Id }, housing);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHousing(
        int id,
        [FromBody] HousingInfoDto housing)
    {
        if (id != housing.Id)
            return BadRequest();
        await service.UpdateHousing(housing);
        return Ok(housing);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHousing(int id)
    {
        await service.DeleteHousing(id);
        return NoContent();
    }
}