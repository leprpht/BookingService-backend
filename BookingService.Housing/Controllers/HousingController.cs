using BookingService.Housing.Models;
using BookingService.Housing.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/housing")]
public class HousingController(IHousingService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetHousings()
    {
        var housings = await service.GetAllHousings();
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

    [HttpPost]
    public async Task<IActionResult> CreateHousing([FromBody] HousingInfo housing)
    {
        await service.CreateHousing(housing);
        return CreatedAtAction(nameof(GetHousing), new { id = housing.Id }, housing);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHousing(
        int id,
        [FromBody] HousingInfo housing)
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