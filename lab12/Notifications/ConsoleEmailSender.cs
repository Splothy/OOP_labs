using lab12.Interfaces;
namespace lab12.Notifications;

public class ConsoleEmailSender : IEmailSender
{
    public void Send(string recipient, string subject, string body)
    {
        Console.WriteLine($"\n[Імітація email] Кому: {recipient}\nТема: {subject}\n{body}");
    }
}
