using System.Net;
using System.Net.Mail;

namespace lab12;

public class SmtpEmailSender : IEmailSender
{
    private readonly string host;
    private readonly int port;
    private readonly string from;
    private readonly string? username;
    private readonly string? password;
    private readonly bool enableSsl;

    public SmtpEmailSender(string host, int port, string from,
        string? username, string? password, bool enableSsl)
    {
        this.host = host;
        this.port = port;
        this.from = from;
        this.username = username;
        this.password = password;
        this.enableSsl = enableSsl;
    }

    public void Send(string recipient, string subject, string body)
    {
        using var client = new SmtpClient(host, port);
        client.EnableSsl = enableSsl;
        client.Timeout = 10000;
        if (!string.IsNullOrWhiteSpace(username))
            client.Credentials = new NetworkCredential(username, password);

        using var message = new MailMessage(from, recipient, subject, body);
        client.Send(message);
    }
}
