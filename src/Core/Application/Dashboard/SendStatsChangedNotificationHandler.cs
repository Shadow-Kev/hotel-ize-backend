using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Identity;
using FSH.WebApi.Domain.Ize;
using FSH.WebApi.Shared.Events;

namespace FSH.WebApi.Application.Dashboard;

public class SendStatsChangedNotificationHandler :
    IEventNotificationHandler<EntityCreatedEvent<Brand>>,
    IEventNotificationHandler<EntityDeletedEvent<Brand>>,
    IEventNotificationHandler<EntityCreatedEvent<Product>>,
    IEventNotificationHandler<EntityDeletedEvent<Product>>,
    IEventNotificationHandler<EntityCreatedEvent<Client>>,
    IEventNotificationHandler<EntityDeletedEvent<Client>>,
    IEventNotificationHandler<EntityCreatedEvent<Chambre>>,
    IEventNotificationHandler<EntityDeletedEvent<Chambre>>,
    IEventNotificationHandler<EntityCreatedEvent<TypeChambre>>,
    IEventNotificationHandler<EntityDeletedEvent<TypeChambre>>,
    IEventNotificationHandler<EntityCreatedEvent<TypeReservation>>,
    IEventNotificationHandler<EntityDeletedEvent<TypeReservation>>,
    IEventNotificationHandler<EntityCreatedEvent<Reservation>>,
    IEventNotificationHandler<EntityDeletedEvent<Reservation>>,
    IEventNotificationHandler<EntityCreatedEvent<Agent>>,
    IEventNotificationHandler<EntityDeletedEvent<Agent>>,
    IEventNotificationHandler<ApplicationRoleCreatedEvent>,
    IEventNotificationHandler<ApplicationRoleDeletedEvent>,
    IEventNotificationHandler<ApplicationUserCreatedEvent>
{
    private readonly ILogger<SendStatsChangedNotificationHandler> _logger;
    private readonly INotificationSender _notifications;

    public SendStatsChangedNotificationHandler(ILogger<SendStatsChangedNotificationHandler> logger, INotificationSender notifications) =>
        (_logger, _notifications) = (logger, notifications);

    public Task Handle(EventNotification<EntityCreatedEvent<Brand>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityDeletedEvent<Brand>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityCreatedEvent<Product>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityDeletedEvent<Product>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<ApplicationRoleCreatedEvent> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<ApplicationRoleDeletedEvent> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<ApplicationUserCreatedEvent> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityCreatedEvent<Client>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityDeletedEvent<Client>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityCreatedEvent<Chambre>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityDeletedEvent<Chambre>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityCreatedEvent<TypeChambre>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityDeletedEvent<TypeChambre>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityCreatedEvent<TypeReservation>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityDeletedEvent<TypeReservation>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityCreatedEvent<Reservation>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityDeletedEvent<Reservation>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityCreatedEvent<Agent>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityDeletedEvent<Agent>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    private Task SendStatsChangedNotification(IEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered => Sending StatsChangedNotification", @event.GetType().Name);

        return _notifications.SendToAllAsync(new StatsChangedNotification(), cancellationToken);
    }
}