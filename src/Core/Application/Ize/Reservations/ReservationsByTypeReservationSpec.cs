using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations;
public class ReservationsByTypeReservationSpec : Specification<Reservation>
{
    public ReservationsByTypeReservationSpec(Guid typeReservationId) =>
        Query.Where(c => c.TypeReservationId == typeReservationId);
}
