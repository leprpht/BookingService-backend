using BookingService.Database;
using BookingService.Housing.Models;
using BookingService.Notifications.Email;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Notifications;

public class NotificationService(
    BookingServiceDbContext context,
    IEmailService emailService) : INotificationService
{ 
    public async Task SendTripRemindersAsync()
    {
        var tomorrow = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

        var staysToRemind = await context.Stays
            .Where(s => s.Status == StayStatus.Confirmed && s.From == tomorrow)
            .Include(s => s.RoomInstance)
                .ThenInclude(r => r.Unit)
                    .ThenInclude(u => u.Property)
            .Select(s => new
            {
                Stay = s,
                User = context.Users.First(u => u.Id == s.UserId)
            })
            .ToListAsync();

        var reminderTasks = staysToRemind.Select(item =>
        {
            var stay = item.Stay;
            var user = item.User;
            var property = stay.RoomInstance.Unit.Property;
            var unit = stay.RoomInstance.Unit;

            var dto = new TripReminderEmailDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                BookingId = stay.Id,
                PropertyName = property.Name,
                City = property.City,
                State = property.State,
                Country = property.Country,
                UnitName = unit.Name,
                RoomNumber = stay.RoomInstance.RoomNumber,
                CheckIn = stay.From,
                CheckOut = stay.To
            };

            return emailService.SendTripReminderEmailAsync(dto);
        });

        await Task.WhenAll(reminderTasks);
    }

    public async Task SendNewBookingNotificationsAsync(Guid stayId)
    {
        var stay = await context.Stays
            .Include(s => s.RoomInstance)
                .ThenInclude(r => r.Unit)
                    .ThenInclude(u => u.Property)
            .FirstOrDefaultAsync(s => s.Id == stayId);

        if (stay == null) return;

        var guest = await context.Users.FindAsync(stay.UserId);
        if (guest == null) return;

        var property = stay.RoomInstance.Unit.Property;
        var unit = stay.RoomInstance.Unit;
        var host = await context.Users.FindAsync(property.OwnerId);
        
        var guestDto = new BookingConfirmationEmailDto
        {
            Email = guest.Email,
            FirstName = guest.FirstName,
            LastName = guest.LastName,
            BookingId = stay.Id,
            PropertyName = property.Name,
            PropertyAddress = property.Address,
            City = property.City,
            State = property.State,
            Country = property.Country,
            UnitName = unit.Name,
            RoomNumber = stay.RoomInstance.RoomNumber,
            CheckIn = stay.From,
            CheckOut = stay.To,
            TotalPrice = stay.TotalPrice
        };
        
        var guestFullName = string.IsNullOrWhiteSpace(guest.MiddleName)
            ? $"{guest.FirstName} {guest.LastName}"
            : $"{guest.FirstName} {guest.MiddleName} {guest.LastName}";

        var tasks = new List<Task>
        {
            emailService.SendBookingConfirmationToGuestAsync(guestDto)
        };

        if (host != null)
        {
            var hostDto = new HostBookingNotificationEmailDto
            {
                HostEmail = host.Email,
                HostFirstName = host.FirstName,
                GuestFullName = guestFullName,
                BookingId = stay.Id,
                PropertyName = property.Name,
                UnitName = unit.Name,
                RoomNumber = stay.RoomInstance.RoomNumber,
                CheckIn = stay.From,
                CheckOut = stay.To,
                TotalPrice = stay.TotalPrice
            };
            tasks.Add(emailService.SendNewBookingNotificationToHostAsync(hostDto));
        }

        await Task.WhenAll(tasks);
    }

    public async Task SendBookingStatusChangedNotificationAsync(Guid stayId, string newStatus)
    {
        var stay = await context.Stays
            .Include(s => s.RoomInstance)
                .ThenInclude(r => r.Unit)
                    .ThenInclude(u => u.Property)
            .FirstOrDefaultAsync(s => s.Id == stayId);

        if (stay == null) return;

        var guest = await context.Users.FindAsync(stay.UserId);
        if (guest == null) return;

        var property = stay.RoomInstance.Unit.Property;

        var dto = new BookingStatusChangedEmailDto
        {
            Email = guest.Email,
            FirstName = guest.FirstName,
            BookingId = stay.Id,
            PropertyName = property.Name,
            City = property.City,
            Country = property.Country,
            CheckIn = stay.From,
            CheckOut = stay.To,
            NewStatus = newStatus
        };

        await emailService.SendBookingStatusChangedToGuestAsync(dto);
    }
}