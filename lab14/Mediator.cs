using Microsoft.Extensions.DependencyInjection;

namespace lab14;

public class Mediator : IMediator
{
    private readonly IServiceProvider serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = serviceProvider.GetRequiredService(handlerType);
        var method = handlerType.GetMethod("Handle")!;
        return (Task<TResponse>)method.Invoke(handler, [request, cancellationToken])!;
    }

    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        foreach (var handler in serviceProvider.GetServices<INotificationHandler<TNotification>>())
        {
            await handler.Handle(notification, cancellationToken);
        }
    }
}
