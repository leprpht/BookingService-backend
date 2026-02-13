using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Services;
using BookingService.Housing.Services.RangeServices;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Housing.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UnitController(
    IUnitService service,
    IUnitCustomizationService customizationService,
    IUnitPictureService pictureService,
    IUnitAdditionalService additionalService)
    : ControllerBase
{
    [HttpGet("{unitId}")]
    [AllowAnonymous]
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
    [AllowAnonymous]
    public async Task<IActionResult> GetUnitCustomizationsAsync(int unitId)
    {
        var customizations = await customizationService.GetUnitCustomizationsAsync(unitId);
        if (customizations.Count == 0)
        {
            return NotFound();
        }
        return Ok(customizations);
    }

    [HttpGet("{unitId}/services")]
    [AllowAnonymous]
    public async Task<IActionResult> GetUnitAdditionalServicesAsync(int unitId)
    {
        var services = await additionalService.GetUnitAdditionalServicesAsync(unitId);
        if (services.Count == 0)
        {
            return NotFound();
        }
        return Ok(services);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUnitAsync(
        [FromBody] UnitCreationDto createUnitDto)
    {
        await service.CreateAsync(createUnitDto);
        return Created();
    }

    [HttpPost("{id}/customizations")]
    public async Task<IActionResult> AddUnitCustomizationsAsync(
        int id,
        [FromBody] List<UnitCustomizationCreationDto> creationList)
    {
        await customizationService.AddRangeAsync(id, creationList);
        return Created();
    }
    
    [HttpPost("{id}/pictures")]
    public async Task<IActionResult> AddUnitPicturesAsync(
        int id,
        [FromBody] List<UnitPictureCreationDto> creationList)
    {
        await pictureService.AddRangeAsync(id, creationList);
        return Created();
    }

    [HttpPost("{id}/services")]
    public async Task<IActionResult> AddUnitAdditionalServicesAsync(
        int id,
        [FromBody] UnitAdditionalServicesCreationDto creationDto)
    {
        await additionalService.CreateAsync(creationDto);
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

    [HttpPut("{id}/customizations")]
    public async Task<IActionResult> UpdateUnitCustomizationsAsync(
        int id,
        [FromBody] List<UnitCustomizationUpdateDto> updateList)
    {
        await customizationService.UpdateRangeAsync(id, updateList); 
        return Ok();
    }

    [HttpPut("{id}/pictures")]
    public async Task<IActionResult> UpdateUnitPicturesAsync(
        int id,
        [FromBody] List<UnitPictureUpdateDto> updateList)
    {
        await pictureService.UpdateRangeAsync(id, updateList);
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
    
    [HttpPatch("{id}/services")]
    public async Task<IActionResult> UpdateUnitAdditionalServiceAsync(
        int id,
        [FromBody] UnitAdditionalServicesUpdateDto updateDto)
    {
        if (id != updateDto.UnitId)
        {
            return BadRequest("Unit ID mismatch.");
        }
        
        await additionalService.UpdateAsync(updateDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUnitAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }

    [HttpDelete("{id}/customizations")]
    public async Task<IActionResult> DeleteUnitCustomizationsAsync(int id)
    {
        await customizationService.DeleteRangeAsync(id);
        return NoContent();
    }

    [HttpDelete("{id}/pictures")]
    public async Task<IActionResult> DeleteUnitPicturesAsync(int id)
    {
        await pictureService.DeleteRangeAsync(id);
        return NoContent();
    }
}