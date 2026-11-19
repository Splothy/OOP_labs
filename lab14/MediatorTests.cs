using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace lab14;

public class MediatorTests
{
    [Fact]
    public async Task SendRoutesRequestsToTheirHandlers()
    {
        var services = new ServiceCollection();
        services.AddMediator();
        services.AddTransient<IRequestHandler<Ping, string>, PingHandler>();
        services.AddTransient<IRequestHandler<Greeting, string>, GreetingHandler>();
        services.AddTransient<IRequestHandler<Sum, int>, SumHandler>();
        using var provider = services.BuildServiceProvider();
        var sender = provider.GetRequiredService<ISender>();

        Assert.Equal("Pong", await sender.Send(new Ping()));
        Assert.Equal("Hello, Andrii!", await sender.Send(new Greeting("Andrii")));
        Assert.Equal(7, await sender.Send(new Sum(3, 4)));
    }

    [Fact]
    public async Task PublishCallsAllMatchingHandlers()
    {
        var messages = new List<string>();
        var services = new ServiceCollection();
        services.AddMediator();
        services.AddSingleton(messages);
        services.AddTransient<INotificationHandler<Message>, FirstMessageHandler>();
        services.AddTransient<INotificationHandler<Message>, SecondMessageHandler>();
        services.AddTransient<INotificationHandler<OtherMessage>, OtherMessageHandler>();
        using var provider = services.BuildServiceProvider();
        var publisher = provider.GetRequiredService<IPublisher>();

        await publisher.Publish(new Message("Hello"));

        Assert.Equal(new[] { "First: Hello", "Second: Hello" }, messages);
    }

    [Fact]
    public async Task PublishWithoutHandlersCompletes()
    {
        var services = new ServiceCollection();
        services.AddMediator();
        using var provider = services.BuildServiceProvider();

        await provider.GetRequiredService<IMediator>().Publish(new Message("Hello"));
    }

    [Fact]
    public void ServicesAreTransient()
    {
        var services = new ServiceCollection();
        services.AddMediator();
        services.AddTransient<IRequestHandler<Ping, string>, PingHandler>();
        using var provider = services.BuildServiceProvider();

        Assert.NotSame(provider.GetRequiredService<IMediator>(), provider.GetRequiredService<IMediator>());
        Assert.NotSame(provider.GetRequiredService<ISender>(), provider.GetRequiredService<ISender>());
        Assert.NotSame(provider.GetRequiredService<IPublisher>(), provider.GetRequiredService<IPublisher>());
        Assert.NotSame(provider.GetRequiredService<IRequestHandler<Ping, string>>(),
            provider.GetRequiredService<IRequestHandler<Ping, string>>());
    }

    public record Ping : IRequest<string>;
    public record Greeting(string Name) : IRequest<string>;
    public record Sum(int Left, int Right) : IRequest<int>;
    public record Message(string Text) : INotification;
    public record OtherMessage : INotification;

    public class PingHandler : IRequestHandler<Ping, string>
    {
        public Task<string> Handle(Ping request, CancellationToken cancellationToken = default)
            => Task.FromResult("Pong");
    }

    public class GreetingHandler : IRequestHandler<Greeting, string>
    {
        public Task<string> Handle(Greeting request, CancellationToken cancellationToken = default)
            => Task.FromResult($"Hello, {request.Name}!");
    }

    public class SumHandler : IRequestHandler<Sum, int>
    {
        public Task<int> Handle(Sum request, CancellationToken cancellationToken = default)
            => Task.FromResult(request.Left + request.Right);
    }

    public class FirstMessageHandler(List<string> messages) : INotificationHandler<Message>
    {
        public Task Handle(Message notification, CancellationToken cancellationToken = default)
        {
            messages.Add($"First: {notification.Text}");
            return Task.CompletedTask;
        }
    }

    public class SecondMessageHandler(List<string> messages) : INotificationHandler<Message>
    {
        public Task Handle(Message notification, CancellationToken cancellationToken = default)
        {
            messages.Add($"Second: {notification.Text}");
            return Task.CompletedTask;
        }
    }

    public class OtherMessageHandler(List<string> messages) : INotificationHandler<OtherMessage>
    {
        public Task Handle(OtherMessage notification, CancellationToken cancellationToken = default)
        {
            messages.Add("Other");
            return Task.CompletedTask;
        }
    }
}
