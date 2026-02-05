using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Services;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnitController(IUnitService unitService) : ControllerBase
{
    [HttpGet("{unitId}")]
    public async Task<IActionResult> GetUnitDetailsAsync(
        int unitId,
        [FromBody] PeriodRequest periodRequest)
    {
        var unit = await unitService.GetUnitDetailsAsync(unitId, periodRequest);
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
        await unitService.CreateUnitAsync(createUnitDto);
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

        await unitService.UpdateUnitAsync(unitUpdateDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUnitAsync(int id)
    {
        await unitService.DeleteUnitAsync(id);
        return NoContent();
    }
}