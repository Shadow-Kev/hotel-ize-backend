using FSH.WebApi.Application.Ize.Chambres;
using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Enums;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations;
public class CreateReservationRequest : IRequest<Guid>
{
    public string Nom { get; set; } = default!;
    public string Prenom { get; set; } = default!;
    public Guid? ChambreId { get; set; }
    public Statut StatutReservation { get; set; }
    public Guid TypeReservationId { get; set; }
    public DateTime? DateArrive { get; set; }
}

public class CreateReservationRequestHandler : IRequestHandler<CreateReservationRequest, Guid>
{
    private readonly IRepository<Reservation> _repository;
    private readonly IMediator _mediator;

    public CreateReservationRequestHandler(IRepository<Reservation> repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateReservationRequest request, CancellationToken cancellationToken)
    {

        var reservation = new Reservation(request.Nom, request.Prenom, request.ChambreId, request.TypeReservationId, request.StatutReservation,  request.DateArrive);
        if(request.StatutReservation == Statut.RESERVE && request.ChambreId != null && request.DateArrive == DateTime.Today)
        {
            await _mediator.Send(new UpdateChambreStatutRequest
            {
                Id = (DefaultIdType)request.ChambreId,
                Disponible = false
            });
        }
        if (request.StatutReservation == Statut.TERMINE && request.ChambreId != null)
        {
            await _mediator.Send(new UpdateChambreStatutRequest
            {
                Id = (DefaultIdType)request.ChambreId,
                Disponible = true
            });
        }
        reservation.DomainEvents.Add(EntityCreatedEvent.WithEntity(reservation));
        await _repository.AddAsync(reservation, cancellationToken);
        return reservation.Id;
    }
}