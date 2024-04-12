using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class VenteUpdatedEventHandler : EventNotificationHandler<EntityUpdatedEvent<Vente>>
{
    private readonly ILogger<VenteUpdatedEventHandler> _logger;

    public VenteUpdatedEventHandler(ILogger<VenteUpdatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityUpdatedEvent<Vente> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}
