using System.Security.Claims;
using BookingService.Profile.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Profile.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpGet]
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
}