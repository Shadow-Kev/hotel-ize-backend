using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations.EventHandlers;
public class ReservationUpdatedEventHandler : EventNotificationHandler<EntityUpdatedEvent<Reservation>>
{
    private readonly ILogger<ReservationUpdatedEventHandler> _logger;

    public ReservationUpdatedEventHandler(ILogger<ReservationUpdatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityUpdatedEvent<Reservation> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}
