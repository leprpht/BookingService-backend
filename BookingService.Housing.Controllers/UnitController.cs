using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Services;
using BookingService.Housing.Services.Subservices;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnitController(
    IUnitService service,
    IUnitCustomizationService customizationService,
    IUnitPictureService pictureService)
    : ControllerBase
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

    [HttpGet("{unitId}/customizations")]
    public async Task<IActionResult> GetUnitCustomizationsAsync(int unitId)
    {
        var customizations = await customizationService.GetUnitCustomizationsAsync(unitId);
        if (customizations.Count == 0)
        {
            return NotFound();
        }
        return Ok(customizations);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateUnitAsync(
        [FromBody] UnitCreationDto createUnitDto)
    {
        await service.CreateAsync(createUnitDto);
        return Created();
    }

    [Authorize]
    [HttpPost("{id}/customizations")]
    public async Task<IActionResult> AddUnitCustomizationsAsync(
        int id,
        [FromBody] List<UnitCustomizationCreationDto> creationList)
    {
        await customizationService.AddRangeAsync(id, creationList);
        return Created();
    }

    [Authorize]
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

    [Authorize]
    [HttpPut("{id}/customizations")]
    public async Task<IActionResult> UpdateUnitCustomizationsAsync(
        int id,
        [FromBody] List<UnitCustomizationUpdateDto> updateList)
    {
        await customizationService.UpdateRangeAsync(id, updateList); 
        return Ok();
    }

    [Authorize]
    [HttpPut("{id}/pictures")]
    public async Task<IActionResult> UpdateUnitPicturesAsync(
        int id,
        [FromBody] List<UnitPictureUpdateDto> updateList)
    {
        await pictureService.UpdateRangeAsync(id, updateList);
        return Ok();
    }
    
    [Authorize]
    [HttpPatch("{id}/name")]
    public async Task<IActionResult> UpdateUnitNameAsync(
        int id,
        [FromBody] string newName)
    {
        await service.UpdateNameAsync(id, newName);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUnitAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
    
    [Authorize]
    [HttpDelete("{id}/customizations")]
    public async Task<IActionResult> DeleteUnitCustomizationsAsync(int id)
    {
        await customizationService.DeleteRangeAsync(id);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}/pictures")]
    public async Task<IActionResult> DeleteUnitPicturesAsync(int id)
    {
        await pictureService.DeleteRangeAsync(id);
        return NoContent();
    }
}