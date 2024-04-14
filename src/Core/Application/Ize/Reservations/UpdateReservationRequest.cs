using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Enums;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations;
public class UpdateReservationRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = default!;
    public string Prenom { get; set; } = default!;
    public Guid? ChambreId { get; set; }
    public Statut StatutReservation { get; set; }
    public Guid TypeReservationId { get; set; }
    public DateTime? DateArrive { get; private set; }

}

public class UpdateReservationRequestHandler : IRequestHandler<UpdateReservationRequest, Guid>
{
    private readonly IRepository<Reservation> _repository;
    private readonly IStringLocalizer _localizer;

    public UpdateReservationRequestHandler(IRepository<Reservation> repository, IStringLocalizer<UpdateReservationRequestHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task<Guid> Handle(UpdateReservationRequest request, CancellationToken cancellationToken)
    {
        var reservation = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = reservation ?? throw new NotFoundException(_localizer["Reservation {0} non trouvé", request.Id]);

        var updatedReservation = reservation.Update(request.Nom, request.Prenom, request.ChambreId, request.TypeReservationId, request.StatutReservation, request.DateArrive);
        reservation.DomainEvents.Add(EntityUpdatedEvent.WithEntity(reservation));
        await _repository.UpdateAsync(updatedReservation, cancellationToken);

        return request.Id;
    }
}

