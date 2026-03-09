using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.Location.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController(ILocationNormalizationService service) : ControllerBase
{
    [HttpGet("autocomplete")]
    [SwaggerOperation(
        Summary = "Autocomplete location search",
        Description = "Returns a list of locations matching the query for autocomplete functionality.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(400)]
    public async Task<IActionResult> AutocompleteAsync(
        [FromQuery] string query,
        [FromQuery] int maxResults = 5)
    {
        if (string.IsNullOrWhiteSpace(query) || query.Length < 3)
            return BadRequest(new { message = "Query must be at least 3 characters long" });
        
        if (maxResults is < 1 or > 20)
            return BadRequest(new { message = "Max results must be between 1 and 20" });
        
        var results = await service.AutocompleteAsync(query, maxResults);
        return Ok(results);
    }
}