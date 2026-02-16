using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace BookingService.Shared.Utils;

public static partial class EmailValidator
{
    [GeneratedRegex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", RegexOptions.IgnoreCase)]
    private static partial Regex EmailRegex();

    public static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        email = email.Trim();
        
        try
        {
            var addr = new MailAddress(email);
            if (addr.Address != email)
                return false;
        }
        catch
        {
            return false;
        }
        
        return EmailRegex().IsMatch(email)
               && !email.Contains("..")
               && !email.StartsWith('.')
               && !email.EndsWith('.')
               && !email.Contains(' ');
    }
}