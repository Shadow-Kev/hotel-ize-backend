using FSH.WebApi.Application.Ize.Chambres;
using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations;
public class DeleteReservationRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteReservationRequest(Guid id) => Id = id;
}

public class DeleteReservationRequestHandler : IRequestHandler<DeleteReservationRequest, Guid>
{
    private readonly IRepository<Reservation> _repository;
    private readonly IMediator _mediator;
    private readonly IStringLocalizer _localizer;

    public DeleteReservationRequestHandler(IRepository<Reservation> repository, IStringLocalizer<DeleteReservationRequestHandler> localizer, IMediator mediator)
    {
        _repository = repository;
        _localizer = localizer;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(DeleteReservationRequest request, CancellationToken cancellationToken)
    {
        var reservation = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = reservation ?? throw new NotFoundException(_localizer["Reservation {0} non trouvé", request.Id]);
        reservation.DomainEvents.Add(EntityDeletedEvent.WithEntity(reservation));
        await _repository.DeleteAsync(reservation, cancellationToken);
        await _mediator.Send(new UpdateChambreStatutRequest
        {
            Id = (Guid)reservation.ChambreId,
            Disponible = true
        });
        return request.Id;
    }
}