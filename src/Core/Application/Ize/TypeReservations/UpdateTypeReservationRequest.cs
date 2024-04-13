using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeReservations;
public class UpdateTypeReservationRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Libelle { get; set; } = default!;
}

public class UpdateTypeReservationRequestHandler : IRequestHandler<UpdateTypeReservationRequest, Guid>
{
    private readonly IRepository<TypeReservation> _repository;
    private readonly IStringLocalizer _localizer;

    public UpdateTypeReservationRequestHandler(IRepository<TypeReservation> repository, IStringLocalizer<UpdateTypeReservationRequestHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task<Guid> Handle(UpdateTypeReservationRequest request, CancellationToken cancellationToken)
    {
        var typeReservation = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = typeReservation ?? throw new NotFoundException(_localizer["Type Reservation {0} non trouvé", request.Id]);
        var updatedTypeReservation = typeReservation.Update(request.Libelle);
        typeReservation.DomainEvents.Add(EntityUpdatedEvent.WithEntity(typeReservation));
        await _repository.UpdateAsync(updatedTypeReservation, cancellationToken);

        return request.Id;
    }
}