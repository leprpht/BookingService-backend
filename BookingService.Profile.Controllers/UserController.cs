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
        
        var userId = Guid.Parse(userIdClaim.Value);
        var user = await service.GetByIdAsync(userId);
        
        return user == null
            ? NotFound()
            : Ok(user);
    }

    [HttpPatch]
    [SwaggerOperation(
        Summary = "Update user profile",
        Description = "Updates the profile information of the currently authenticated user.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateUser([FromBody] UserInfoUpdate userUpdate)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        
        
        return NoContent();
    }
    
    [HttpPatch("name")]
    [SwaggerOperation(
        Summary = "Update user name",
        Description = "Updates the name of the currently authenticated user.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateName([FromBody] UserNameDto name)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var userId = Guid.Parse(userIdClaim.Value);
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
        
        var userId = Guid.Parse(userIdClaim.Value);
        await service.UpdateUserEmailAsync(userId, email);
        
        return NoContent();
    }
    
    [HttpPatch("profile-picture")]
    [SwaggerOperation(
        Summary = "Update user profile picture",
        Description = "Updates the profile picture of the currently authenticated user.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpdateProfilePicture([FromBody] string profilePictureUrl)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var userId = Guid.Parse(userIdClaim.Value);
        await service.UpdateProfilePictureAsync(userId, profilePictureUrl);
        
        return NoContent();
    }

    [HttpPatch("upgrade-to-host")]
    [SwaggerOperation(
        Summary = "Upgrade user to host",
        Description =
            "Upgrades the currently authenticated user to a host, allowing them to list properties for rent.")]
    [SwaggerResponse(204)]
    [SwaggerResponse(400)]
    [SwaggerResponse(401)]
    [SwaggerResponse(404)]
    public async Task<IActionResult> UpgradeToHost()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var userId = Guid.Parse(userIdClaim.Value);
        await service.UpgradeToHostAsync(userId);
        
        return NoContent();
    }
}