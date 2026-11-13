namespace lab12;

public class ConsoleEmailSender : IEmailSender
{
    public void Send(string recipient, string subject, string body)
    {
        Console.WriteLine($"\n[Імітація email] Кому: {recipient}\nТема: {subject}\n{body}");
    }
}
