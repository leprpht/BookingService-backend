using System.Security.Claims;
using BookingService.Profile.Dtos;
using BookingService.Profile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.Profile.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get current user profile",
        Description = "Returns information about the currently authenticated user.")]
    [SwaggerResponse(200)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> GetUser()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var userId = int.Parse(userIdClaim.Value);
        var user = await service.GetByIdAsync(userId);
        
        return user == null
            ? NotFound()
            : Ok(user);
    }
    
    [HttpPatch("name")]
    [SwaggerOperation(
        Summary = "Update user name",
        Description = "Updates the name of the currently authenticated user.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateName([FromBody] UserNameUpdate name)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var userId = int.Parse(userIdClaim.Value);
        await service.UpdateUserNameAsync(userId, name);
        
        return NoContent();
    }
    
    [HttpPatch("email")]
    [SwaggerOperation(
        Summary = "Update user email",
        Description = "Updates the email of the currently authenticated user.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    [SwaggerResponse(409)]
    public async Task<IActionResult> UpdateEmail([FromBody] string email)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var userId = int.Parse(userIdClaim.Value);
        await service.UpdateUserEmailAsync(userId, email);
        
        return NoContent();
    }
}