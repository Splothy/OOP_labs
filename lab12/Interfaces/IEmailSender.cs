namespace lab12.Interfaces;

public interface IEmailSender
{
    void Send(string recipient, string subject, string body);
}
