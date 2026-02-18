using BookingService.Housing.DTOs.Unit;
using BookingService.Housing.Services;
using BookingService.Housing.Services.RangeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a unit",
        Description = "Creates a new unit within a property.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> CreateUnitAsync(
        [FromBody] UnitCreationDto createUnitDto)
    {
        await service.CreateAsync(createUnitDto);
        return Created();
    }

    [HttpPost("{id}/customizations")]
    [SwaggerOperation(
        Summary = "Add unit customizations",
        Description = "Adds customizations (amenities, features) to a unit.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> AddUnitCustomizationsAsync(
        int id,
        [FromBody] List<UnitCustomizationCreationDto> creationList)
    {
        await customizationService.AddRangeAsync(id, creationList);
        return Created();
    }
    
    [HttpPost("{id}/pictures")]
    [SwaggerOperation(
        Summary = "Add unit pictures",
        Description = "Adds pictures to a unit.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> AddUnitPicturesAsync(
        int id,
        [FromBody] List<UnitPictureCreationDto> creationList)
    {
        await pictureService.AddRangeAsync(id, creationList);
        return Created();
    }

    [HttpPost("{id}/services")]
    [SwaggerOperation(
        Summary = "Add unit additional services",
        Description = "Adds additional services to a unit.")]
    [SwaggerResponse(201)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> AddUnitAdditionalServicesAsync(
        int id,
        [FromBody] UnitAdditionalServicesCreationDto creationDto)
    {
        await additionalService.CreateAsync(creationDto);
        return Created();
    }

    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update a unit",
        Description = "Updates an existing unit's details.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
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
    [SwaggerOperation(
        Summary = "Update unit customizations",
        Description = "Updates the customizations of an existing unit.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> UpdateUnitCustomizationsAsync(
        int id,
        [FromBody] List<UnitCustomizationUpdateDto> updateList)
    {
        await customizationService.UpdateRangeAsync(id, updateList); 
        return Ok();
    }

    [HttpPut("{id}/pictures")]
    [SwaggerOperation(
        Summary = "Update unit pictures",
        Description = "Updates the pictures of an existing unit.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> UpdateUnitPicturesAsync(
        int id,
        [FromBody] List<UnitPictureUpdateDto> updateList)
    {
        await pictureService.UpdateRangeAsync(id, updateList);
        return Ok();
    }

    [HttpPatch("{id}/name")]
    [SwaggerOperation(
        Summary = "Update unit name",
        Description = "Updates the name of an existing unit.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateUnitNameAsync(
        int id,
        [FromBody] string newName)
    {
        await service.UpdateNameAsync(id, newName);
        return Ok();
    }
    
    [HttpPatch("{id}/services")]
    [SwaggerOperation(
        Summary = "Update unit additional service",
        Description = "Updates additional services for an existing unit.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
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
    [SwaggerOperation(
        Summary = "Delete a unit",
        Description = "Deletes an existing unit and all related data.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> DeleteUnitAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }

    [HttpDelete("{id}/customizations")]
    [SwaggerOperation(
        Summary = "Delete unit customizations",
        Description = "Deletes all customizations for a unit.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> DeleteUnitCustomizationsAsync(int id)
    {
        await customizationService.DeleteRangeAsync(id);
        return NoContent();
    }

    [HttpDelete("{id}/pictures")]
    [SwaggerOperation(
        Summary = "Delete unit pictures",
        Description = "Deletes all pictures for a unit.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    public async Task<IActionResult> DeleteUnitPicturesAsync(int id)
    {
        await pictureService.DeleteRangeAsync(id);
        return NoContent();
    }
}