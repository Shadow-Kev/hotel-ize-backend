using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres.EventHandlers;
public class ChambreDeletedEventHandler : EventNotificationHandler<EntityDeletedEvent<Chambre>>
{
    private readonly ILogger<ChambreDeletedEventHandler> _logger;

    public ChambreDeletedEventHandler(ILogger<ChambreDeletedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityDeletedEvent<Chambre> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}
