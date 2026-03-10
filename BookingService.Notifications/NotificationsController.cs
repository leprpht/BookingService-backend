using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BookingService.Notifications;

[ApiController]
[Route("internal/[controller]")]
public class NotificationsController(IConfiguration configuration) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendNotifications(
        [FromHeader(Name = "Api-Key")] string apiKey)
    {
        await Task.Delay(100);
        return apiKey != configuration["ApiKey"] ? Unauthorized() : Ok();
    }
}