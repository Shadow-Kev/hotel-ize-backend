using FSH.WebApi.Application.Ize.Reservations;
using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeReservations;
public class DeleteTypeReservationRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public DeleteTypeReservationRequest(Guid id) => Id = id;
}

public class DeleteTypeReservationRequestHandler : IRequestHandler<DeleteTypeReservationRequest, Guid>
{
    private readonly IRepositoryWithEvents<TypeReservation> _typeReservationRepo;
    private readonly IRepository<Reservation> _reservationRepo;
    private readonly IStringLocalizer _localizer;

    public DeleteTypeReservationRequestHandler(IRepositoryWithEvents<TypeReservation> typeReservationRepo, IRepository<Reservation> reservationRepo, IStringLocalizer<DeleteTypeReservationRequestHandler> localizer)
    {
        _typeReservationRepo = typeReservationRepo;
        _reservationRepo = reservationRepo;
        _localizer = localizer;
    }
    public async Task<Guid> Handle(DeleteTypeReservationRequest request, CancellationToken cancellationToken)
    {
        if (await _reservationRepo.AnyAsync(new ReservationsByTypeReservationSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_localizer["Type Reservation ne peut pas être supprimé"]);
        }
        var typeReservation = await _typeReservationRepo.GetByIdAsync(request.Id, cancellationToken);
        _ = typeReservation ?? throw new NotFoundException(_localizer["Type Reservation {0} non trouvé", request.Id]);
        typeReservation.DomainEvents.Add(EntityDeletedEvent.WithEntity(typeReservation));
        await _typeReservationRepo.DeleteAsync(typeReservation, cancellationToken);
        return request.Id;
    }
}
