using FSH.WebApi.Application.Ize.Reservations;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations;
public class SearchReservationsRequest : PaginationFilter, IRequest<PaginationResponse<ReservationDto>>
{
    public string Nom { get; private set; } = default!;
    public string Prenom { get; private set; } = default!;
}

public class SearchReservationsRequestHandler : IRequestHandler<SearchReservationsRequest, PaginationResponse<ReservationDto>>
{
    private readonly IReadRepository<Reservation> _repository;
    private readonly IReadRepository<TypeReservation> _typeReservationRepository;

    public SearchReservationsRequestHandler(IReadRepository<Reservation> repository, IReadRepository<TypeReservation> typeReservationRepository) => (_repository, _typeReservationRepository) = (repository, typeReservationRepository);

    public async Task<PaginationResponse<ReservationDto>> Handle(SearchReservationsRequest request, CancellationToken cancellationToken)
    {
       throw new NotImplementedException();
    }

}
