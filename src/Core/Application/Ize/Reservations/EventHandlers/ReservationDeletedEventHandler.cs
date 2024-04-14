using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations.EventHandlers;
public class ReservationDeletedEventHandler : EventNotificationHandler<EntityDeletedEvent<Reservation>>
{
    private readonly ILogger<ReservationDeletedEventHandler> _logger;

    public ReservationDeletedEventHandler(ILogger<ReservationDeletedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityDeletedEvent<Reservation> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}

