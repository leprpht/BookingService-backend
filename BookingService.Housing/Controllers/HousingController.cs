using BookingService.Housing.DTOs;
using BookingService.Housing.Services;
using BookingService.Housing.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/housing")]
public class HousingController(IHousingService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetHousing(
        int id,
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to)
    {
        var housing = await service.GetHousingById(id, from, to);
        if (housing is null)
            return NotFound();
        return Ok(housing);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetHousingsByFilters(
        [FromQuery] DateOnly from,
        [FromQuery] DateOnly to,
        [FromQuery] string? name,
        [FromQuery] string? city,
        [FromQuery] string? country,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        var filter = new FilterOptions
        {
            From = from,
            To = to,
            Name = name,
            City = city,
            Country = country,
            MinPrice = minPrice,
            MaxPrice = maxPrice
        };
        var housings = await service.GetHousingsByFilters(filter, page, pageSize);
        return Ok(housings);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHousing([FromBody] HousingCreationDto housing)
    {
        await service.CreateHousing(housing);
        return CreatedAtAction(nameof(GetHousing), housing);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHousing(
        int id,
        [FromBody] HousingUpdateDto housing)
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

    [HttpGet("stay/{id}")]
    public async Task<IActionResult> GetHousingByStayId(int id)
    {
        var housing = await service.GetHousingByStayId(id);
        if (housing is null)
            return NotFound();
        return Ok(housing);
    }
}