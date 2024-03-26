using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Clients.EventHandlers;
public class ClientUpdatedEventHandler : EventNotificationHandler<EntityUpdatedEvent<Client>>
{
    private readonly ILogger<ClientUpdatedEventHandler> _logger;

    public ClientUpdatedEventHandler(ILogger<ClientUpdatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityUpdatedEvent<Client> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}
