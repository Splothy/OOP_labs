using lab12.Interfaces;
using System.Net.Mail;

namespace lab12.Validation;

public class EmailValidator : IEmailValidator
{
    public bool IsValid(string email)
    {
        return MailAddress.TryCreate(email, out var address) &&
               address.Address.Equals(email, StringComparison.OrdinalIgnoreCase);
    }
}
