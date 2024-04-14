using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeReservations;
public class CreateTypeReservationRequest : IRequest<Guid>
{
    public string Libelle { get; set; } = default!;
}

public class CreateTypeReservationRequestHandler : IRequestHandler<CreateTypeReservationRequest, Guid>
{
    private readonly IRepository<TypeReservation> _repository;

    public CreateTypeReservationRequestHandler(IRepository<TypeReservation> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateTypeReservationRequest request, CancellationToken cancellationToken)
    {
        var typeReservation = new TypeReservation(request.Libelle);
        typeReservation.DomainEvents.Add(EntityCreatedEvent.WithEntity(typeReservation));
        await _repository.AddAsync(typeReservation, cancellationToken);
        return typeReservation.Id;
    }
}