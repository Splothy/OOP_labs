using System.Text;
using lab12.Interfaces;
using lab12.Notifications;
using lab12.Services;
using lab12.UI;
using lab12.Validation;
using Microsoft.Extensions.DependencyInjection;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

var services = new ServiceCollection();
services.AddScoped<IEmailValidator, EmailValidator>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<ISubscriptionService, SubscriptionService>();
services.AddScoped<IBookObserver, EmailBookObserver>();
services.AddScoped<IBookCatalog, BookCatalog>();
services.AddScoped<LibraryConsole>();

string? smtpHost = Environment.GetEnvironmentVariable("LIBRARY_SMTP_HOST");
if (string.IsNullOrWhiteSpace(smtpHost))
{
    services.AddScoped<IEmailSender, ConsoleEmailSender>();
    Console.WriteLine("SMTP не налаштовано: email-повідомлення імітуються в консолі.");
}
else
{
    string? from = Environment.GetEnvironmentVariable("LIBRARY_SMTP_FROM");
    if (string.IsNullOrWhiteSpace(from) || !new EmailValidator().IsValid(from))
    {
        Console.WriteLine("Укажіть коректну адресу відправника в LIBRARY_SMTP_FROM.");
        return;
    }

    string? portSetting = Environment.GetEnvironmentVariable("LIBRARY_SMTP_PORT");
    int port = 587;
    if (portSetting is not null && (!int.TryParse(portSetting, out port) || port is < 1 or > 65535))
    {
        Console.WriteLine("LIBRARY_SMTP_PORT має бути числом від 1 до 65535.");
        return;
    }

    string? sslSetting = Environment.GetEnvironmentVariable("LIBRARY_SMTP_SSL");
    bool ssl = true;
    if (sslSetting is not null && !bool.TryParse(sslSetting, out ssl))
    {
        Console.WriteLine("LIBRARY_SMTP_SSL має бути true або false.");
        return;
    }

    services.AddScoped<IEmailSender>(_ => new SmtpEmailSender(smtpHost, port, from,
        Environment.GetEnvironmentVariable("LIBRARY_SMTP_USERNAME"),
        Environment.GetEnvironmentVariable("LIBRARY_SMTP_PASSWORD"), ssl));
    Console.WriteLine("Увімкнено надсилання email через SMTP.");
}

using var provider = services.BuildServiceProvider(new ServiceProviderOptions
{
    ValidateScopes = true,
    ValidateOnBuild = true
});
using var scope = provider.CreateScope();
scope.ServiceProvider.GetRequiredService<LibraryConsole>().Run();
