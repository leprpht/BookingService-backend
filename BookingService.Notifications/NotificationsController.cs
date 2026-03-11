using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BookingService.Notifications;

[ApiController]
[Route("internal/[controller]")]
public class NotificationsController(INotificationService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendNotifications(
        /* [FromHeader(Name = "Api-Key")] string apiKey */)
    {
        await service.SendTripRemindersAsync();
        return Ok();
    }
}