using Microsoft.AspNetCore.SignalR;

namespace BookingService.Hubs;

public class BookingHub : Hub
{
    public async Task JoinPropertyGroup(string propertyId) =>
        await Groups.AddToGroupAsync(Context.ConnectionId, $"property-{propertyId}");
    
    public async Task LeavePropertyGroup(int propertyId) =>
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"property-{propertyId}");
}