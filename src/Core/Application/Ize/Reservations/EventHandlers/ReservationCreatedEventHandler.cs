using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations.EventHandlers;
public class ReservationCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<Reservation>>
{
    private readonly ILogger<ReservationCreatedEventHandler> _logger;

    public ReservationCreatedEventHandler(ILogger<ReservationCreatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityCreatedEvent<Reservation> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}