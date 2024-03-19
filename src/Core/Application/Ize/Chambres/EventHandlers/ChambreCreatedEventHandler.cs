using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres.EventHandlers;
public class ChambreCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<Chambre>>
{
    private readonly ILogger<ChambreCreatedEventHandler> _logger;

    public ChambreCreatedEventHandler(ILogger<ChambreCreatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityCreatedEvent<Chambre> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}
