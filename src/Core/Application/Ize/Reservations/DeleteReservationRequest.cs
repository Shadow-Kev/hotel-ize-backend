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
    private readonly IStringLocalizer _localizer;

    public DeleteReservationRequestHandler(IRepository<Reservation> repository, IStringLocalizer<DeleteReservationRequestHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task<Guid> Handle(DeleteReservationRequest request, CancellationToken cancellationToken)
    {
        var Reservation = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = Reservation ?? throw new NotFoundException(_localizer["Reservation {0} non trouvé", request.Id]);
        Reservation.DomainEvents.Add(EntityDeletedEvent.WithEntity(Reservation));
        await _repository.DeleteAsync(Reservation, cancellationToken);
        return request.Id;
    }
}