using MassTransit;
using SatisTalepYonetimi.Application.Services;

namespace SatisTalepYonetimi.Infrastructure.Services;

public sealed class MassTransitEventPublisher(IPublishEndpoint publishEndpoint) : IEventPublisher
{
    public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class
    {
        await publishEndpoint.Publish(@event, cancellationToken);
    }
}
