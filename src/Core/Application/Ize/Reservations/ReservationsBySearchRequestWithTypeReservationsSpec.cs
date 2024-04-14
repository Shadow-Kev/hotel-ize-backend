using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations;
public class ReservationsBySearchRequestWithTypeReservationsSpec : EntitiesByPaginationFilterSpec<Reservation, ReservationDto>
{
    public ReservationsBySearchRequestWithTypeReservationsSpec(SearchReservationsRequest request)
        : base(request) =>
        Query
            .Include(r => r.Chambre)
            .OrderBy(r => r.Nom, !request.HasOrderBy())
            .Where(r => r.ChambreId.Equals(request.ChambreId!.Value), request.ChambreId.HasValue);
}
