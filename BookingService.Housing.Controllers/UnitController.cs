using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Services;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnitController(IUnitService service) : ControllerBase
{
    [HttpGet("{unitId}")]
    public async Task<IActionResult> GetUnitDetailsAsync(
        int unitId,
        [FromBody] PeriodRequest periodRequest)
    {
        var unit = await service.GetUnitDetailsAsync(unitId, periodRequest);
        if (unit == null)
        {
            return NotFound();
        }
        return Ok(unit);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUnitAsync(
        [FromBody] UnitCreationDto createUnitDto)
    {
        await service.CreateAsync(createUnitDto);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUnitAsync(
        int id,
        [FromBody] UnitUpdateDto unitUpdateDto)
    {
        if (id != unitUpdateDto.Id)
        {
            return BadRequest("Unit ID mismatch.");
        }

        await service.UpdateAsync(unitUpdateDto);
        return Ok();
    }
    
    [HttpPatch("{id}/name")]
    public async Task<IActionResult> UpdateUnitNameAsync(
        int id,
        [FromBody] string newName)
    {
        await service.UpdateNameAsync(id, newName);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUnitAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}