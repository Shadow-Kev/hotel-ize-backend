using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes.EventHandler;
public class VenteCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<Vente>>
{
    private readonly ILogger<VenteCreatedEventHandler> _logger;

    public VenteCreatedEventHandler(ILogger<VenteCreatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityCreatedEvent<Vente> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}
