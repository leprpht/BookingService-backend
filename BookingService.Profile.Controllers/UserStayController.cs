using System.Security.Claims;
using BookingService.Profile.Model;
using BookingService.Profile.Services;
using BookingService.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Profile.Controllers;

[ApiController]
[Authorize]
[Route("api/User/stays")]
public class UserStayController(IUserStayService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserStays(
        [FromQuery] PageRequest pageRequest,
        [FromQuery] StaySearchFilter? filter = null)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();
        
        var userId = int.Parse(userIdClaim.Value);
        
        filter ??= new StaySearchFilter();
        
        var stays = await service.GetUserStaysAsync(userId, pageRequest, filter);
        return Ok(stays);
    }
}