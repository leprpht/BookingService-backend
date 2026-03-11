namespace BookingService.Notifications.Email;

public static class EmailTemplateLoader
{
    public static string LoadTemplate(string templateName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "EmailTemplates", templateName);
        return File.ReadAllText(path);
    }
}