using FSH.WebApi.Application.Ize.Reservations;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations;
public class SearchReservationsRequest : PaginationFilter, IRequest<PaginationResponse<ReservationDto>>
{
    public string? Nom { get; set; }
    public string? Prenom { get; set; }
    public DateTime? DateArrive { get; set; }
    public Guid? ChambreId { get; set; }
}

public class SearchReservationsRequestHandler : IRequestHandler<SearchReservationsRequest, PaginationResponse<ReservationDto>>
{
    private readonly IReadRepository<Reservation> _repository;

    public SearchReservationsRequestHandler(IReadRepository<Reservation> repository) => _repository = repository;

    public async Task<PaginationResponse<ReservationDto>> Handle(SearchReservationsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ReservationsBySearchRequestWithTypeReservationsSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }

}
