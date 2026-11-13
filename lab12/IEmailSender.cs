namespace lab12;

public interface IEmailSender
{
    void Send(string recipient, string subject, string body);
}
