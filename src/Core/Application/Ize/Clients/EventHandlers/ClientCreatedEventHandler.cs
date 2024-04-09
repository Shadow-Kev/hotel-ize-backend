using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Clients.EventHandlers;
public class ClientCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<Client>>
{
    private readonly ILogger<ClientCreatedEventHandler> _logger;

    public ClientCreatedEventHandler(ILogger<ClientCreatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityCreatedEvent<Client> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}
