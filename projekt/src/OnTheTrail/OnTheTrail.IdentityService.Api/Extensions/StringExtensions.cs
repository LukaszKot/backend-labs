using System.Net.Mail;

namespace OnTheTrail.IdentityService.Api.Extensions;

public static class StringExtensions
{
    public static bool IsValidEmail(this string email)
    {
        try
        {
            var _ = new MailAddress(email);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}