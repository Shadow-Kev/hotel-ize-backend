using FSH.WebApi.Application.Ize.Reservations;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations;
public class GetReservationRequest : IRequest<ReservationDetailDto>
{
    public Guid Id { get; set; }

    public GetReservationRequest(Guid id) => Id = id;
}

public class GetReservationRequestHandler : IRequestHandler<GetReservationRequest, ReservationDetailDto>
{
    private readonly IRepository<Reservation> _repository;
    private readonly IStringLocalizer _t;

    public GetReservationRequestHandler(IRepository<Reservation> repository, IStringLocalizer<GetReservationRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }

    public async Task<ReservationDetailDto> Handle(GetReservationRequest request, CancellationToken cancellationToken) =>
     await _repository.FirstOrDefaultAsync(
           (ISpecification<Reservation, ReservationDetailDto>)new ReservationByIdWithTypeReservationSpec(request.Id), cancellationToken)
       ?? throw new NotFoundException(_t["Reservation {0} non trouvé.", request.Id]);
}
