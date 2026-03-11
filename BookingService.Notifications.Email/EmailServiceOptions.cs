namespace BookingService.Notifications.Email;

public sealed class EmailServiceOptions
{
    public const string SectionName = "Notifications:Email";
    public required string ConnectionString { get; init; }
    public required string SenderAddress { get; init; }
}