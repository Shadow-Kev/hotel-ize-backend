using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres.EventHandlers;
public class ChambreUpdatedEventHandler : EventNotificationHandler<EntityUpdatedEvent<Chambre>>
{
    private readonly ILogger<ChambreUpdatedEventHandler> _logger;

    public ChambreUpdatedEventHandler(ILogger<ChambreUpdatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityUpdatedEvent<Chambre> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}
