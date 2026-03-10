using BookingService.Database;
using BookingService.Housing.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Notifications;

public class NotificationService(BookingServiceDbContext context) : INotificationService
{
    private async Task<List<UserNotificationDto>> GetUsersToSendReminders()
    {
        return await context.Users
            .Where(u => u.Stays
                .Any(s =>
                    s.Status == StayStatus.Confirmed &&
                    s.From == DateOnly.FromDateTime(DateTime.Today.AddDays(1))))
            .Select(u => new UserNotificationDto
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                MiddleName = u.MiddleName,
                LastName = u.LastName,
                Stays = u.Stays.Where(s =>
                        s.Status == StayStatus.Confirmed &&
                        s.From == DateOnly.FromDateTime(DateTime.Today.AddDays(1)))
                    .Select(s => new StayNotificationDto
                    {
                        Id = s.Id,
                        City = s.RoomInstance.Unit.Property.City,
                        State = s.RoomInstance.Unit.Property.State,
                        Country = s.RoomInstance.Unit.Property.Country,
                        From = s.From,
                        To = s.To,
                        PropertyId = s.RoomInstance.Unit.PropertyId,
                        PropertyName = s.RoomInstance.Unit.Property.Name
                    }).ToList()
            }).ToListAsync();
    }

    public async Task SendTripRemindersAsync()
    {
        var usersToNotify = await GetUsersToSendReminders();

        /*
        foreach (var user in usersToNotify)
        {
            Console.WriteLine($"{user.FirstName} {user.LastName} - {user.Email}");
            if (user.Stays.Count == 1)
            {
                Console.WriteLine($"  Reminder: You have a trip to {user.Stays[0].PropertyName} tomorrow!");
            }
            else
            {
                Console.WriteLine($"  Reminder: You have {user.Stays.Count} trips tomorrow!");
                foreach (var stay in user.Stays)
                {
                    Console.WriteLine($"    - {stay.PropertyName} in {stay.City}, {stay.Country} from {stay.From} to {stay.To}");
                }
            }
        }
        */
    }
}