using BookingService.Housing.DTOs;
using BookingService.Housing.Services;
using BookingService.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/housing")]
public class HousingController(IHousingService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetHousing(
        int id,
        [FromQuery] PeriodRequest period)
    {
        var housing = await service.GetHousingById(id, period);
        if (housing is null)
            return NotFound();
        return Ok(housing);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetHousingsByFilters(
        [FromQuery] FilterOptions filter,
        [FromQuery] PageRequest page)
    {
        var housings = await service.GetHousingsByFilters(filter, page);
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