using lab12.Interfaces;
using lab12.Models;
using System.Net.Mail;

namespace lab12.Notifications;

public class EmailBookObserver : IBookObserver
{
    private readonly ISubscriptionService subscriptions;
    private readonly IEmailSender emailSender;

    public EmailBookObserver(ISubscriptionService subscriptions, IEmailSender emailSender)
    {
        this.subscriptions = subscriptions;
        this.emailSender = emailSender;
    }

    public void Update(Book book, string change)
    {
        foreach (var user in subscriptions.GetSubscribers(book.Category))
        {
            try
            {
                emailSender.Send(user.Email, $"Бібліотека: {change.ToLowerInvariant()}",
                    $"{user.Name}, {change.ToLowerInvariant()}: «{book.Title}», " +
                    $"{book.Author}. Категорія: {book.Category}.");
            }
            catch (SmtpException)
            {
                
                Console.WriteLine($"Не вдалося надіслати email на {user.Email}. Перевірте SMTP.");
            }
        }
    }
}
