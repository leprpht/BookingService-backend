using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BookingService.Auth.Policies;

public class IdMatchRequirement : IAuthorizationRequirement;

public class IdMatchHandler : AuthorizationHandler<IdMatchRequirement, Guid>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        IdMatchRequirement requirement,
        Guid resourceId)
    {
        return context.User.FindFirst(ClaimTypes.Role)?.Value == "Admin"
            ? AdminOverride(context, requirement)
            : UserIdMatch(context, requirement, resourceId);
    }
    
    private static Task AdminOverride(AuthorizationHandlerContext context, IdMatchRequirement requirement)
    {
        context.Succeed(requirement);
        return Task.CompletedTask;
    }

    private static Task UserIdMatch(
        AuthorizationHandlerContext context,
        IdMatchRequirement requirement,
        Guid resourceId)
    {
        var userIdClaim = context.User.FindFirst(JwtRegisteredClaimNames.Sub);
        if (userIdClaim == null)
            return Task.CompletedTask;
        
        if (Guid.TryParse(userIdClaim.Value, out var userId) && userId == resourceId)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}