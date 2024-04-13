using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Enums;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations;
public class CreateReservationRequest : IRequest<Guid>
{
    public string Nom { get; set; } = default!;
    public string Prenom { get; set; } = default!;
    public Guid ChambreId { get; set; }
    public Statut StatutReservation { get; set; }
    public Guid TypeReservationId { get; set; }
    public DateTime? DateArrive { get; private set; }
}

public class CreateReservationRequestHandler : IRequestHandler<CreateReservationRequest, Guid>
{
    private readonly IRepository<Reservation> _repository;

    public CreateReservationRequestHandler(IRepository<Reservation> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateReservationRequest request, CancellationToken cancellationToken)
    {

        var reservation = new Reservation(request.Nom, request.Prenom, request.ChambreId, request.TypeReservationId, request.StatutReservation,  request.DateArrive);

        reservation.DomainEvents.Add(EntityCreatedEvent.WithEntity(reservation));

        await _repository.AddAsync(reservation, cancellationToken);
        return reservation.Id;
    }
}