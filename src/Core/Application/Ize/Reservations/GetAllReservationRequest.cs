using FSH.WebApi.Application.Ize.Reservations;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations;
public class GetAllReservationRequest : IRequest<List<ReservationDetailDto>>
{
    public GetAllReservationRequest() { }
}

public class GetAllReservationSpec : Specification<Reservation, ReservationDetailDto>
{
    public GetAllReservationSpec() =>
        Query.Include(c => c.TypeReservation)
        .Include(c => c.Chambre);
}

public class GetAllReservationRequestHandler : IRequestHandler<GetAllReservationRequest, List<ReservationDetailDto>>
{
    private readonly IRepository<Reservation> _repository;
    private readonly IStringLocalizer _t;
    public GetAllReservationRequestHandler(IRepository<Reservation> repository, IStringLocalizer<GetAllReservationRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }

    public async Task<List<ReservationDetailDto>> Handle(GetAllReservationRequest request, CancellationToken cancellationToken) =>
        await _repository.ListAsync<ReservationDetailDto>(new GetAllReservationSpec(), cancellationToken)
        ?? throw new NotFoundException(_t["Aucune REservation trouvé."]);
}