using Microsoft.Extensions.DependencyInjection;

namespace lab14;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddTransient<IMediator, Mediator>();
        services.AddTransient<ISender, Mediator>();
        services.AddTransient<IPublisher, Mediator>();
        return services;
    }
}
