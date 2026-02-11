using Microsoft.AspNetCore.Authorization;

namespace BookingService.Auth;

public interface IAuthorizationHandler
{
    Task HandleAsync(AuthorizationHandlerContext context);
}