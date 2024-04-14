using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeReservations;
public class GetAllTypeReservationRequest : IRequest<List<TypeReservationDto>>
{
    public GetAllTypeReservationRequest() { }
}

public class GetAllTypeReservationSpec : Specification<TypeReservation, TypeReservationDto>
{
}

public class GetAllTypeReservationRequestHandler : IRequestHandler<GetAllTypeReservationRequest, List<TypeReservationDto>>
{
    private readonly IRepository<TypeReservation> _repository;
    private readonly IStringLocalizer _t;
    public GetAllTypeReservationRequestHandler(IRepository<TypeReservation> repository, IStringLocalizer<GetAllTypeReservationRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }

    public async Task<List<TypeReservationDto>> Handle(GetAllTypeReservationRequest request, CancellationToken cancellationToken) =>
        await _repository.ListAsync<TypeReservationDto>(new GetAllTypeReservationSpec(), cancellationToken)
        ?? throw new NotFoundException(_t["Aucun type de reservation trouvé."]);
}