using FSH.WebApi.Application.Ize.Chambres.EventHandlers;
using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Clients.EventHandlers;
public class ClientDeletedEventHandler : EventNotificationHandler<EntityDeletedEvent<Client>>
{
    private readonly ILogger<ClientDeletedEventHandler> _logger;

    public ClientDeletedEventHandler(ILogger<ClientDeletedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityDeletedEvent<Client> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}