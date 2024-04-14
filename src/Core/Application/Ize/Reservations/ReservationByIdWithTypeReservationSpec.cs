using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations;
public class ReservationByIdWithTypeReservationSpec : Specification<Reservation, ReservationDetailDto>, ISingleResultSpecification
{
    public ReservationByIdWithTypeReservationSpec(Guid id) =>
        Query
            .Where(c => c.Id == id)
            .Include(c => c.TypeReservation)
            .Include(c => c.Chambre);
}