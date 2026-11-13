using System.Net.Mail;

namespace lab12;

public class EmailValidator : IEmailValidator
{
    public bool IsValid(string email)
    {
        return MailAddress.TryCreate(email, out var address) &&
               address.Address.Equals(email, StringComparison.OrdinalIgnoreCase);
    }
}
