using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeReservations;
public class GetTypeReservationRequest : IRequest<TypeReservationDto>
{
    public Guid Id { get; set; }
    public GetTypeReservationRequest(Guid id) => Id = id;

}

public class TypeReservationByIdSpec : Specification<TypeReservation, TypeReservationDto>, ISingleResultSpecification
{
    public TypeReservationByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetTypeReservationRequestHandler : IRequestHandler<GetTypeReservationRequest, TypeReservationDto>
{
    private readonly IRepository<TypeReservation> _repository;
    private readonly IStringLocalizer _t;

    public GetTypeReservationRequestHandler(IRepository<TypeReservation> repository, IStringLocalizer<GetTypeReservationRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;

    }
    public async Task<TypeReservationDto> Handle(GetTypeReservationRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<TypeReservation, TypeReservationDto>)new TypeReservationByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Type Reservation {0} non trouvé.", request.Id]);
}
