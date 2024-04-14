using FSH.WebApi.Domain.Enums;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations;
public class ReservationDetailDto : IDto
{
    public Guid Id { get; set; } = default!;
    public string Nom { get; set; } = default!;
    public string Prenom { get; set; } = default!;
    public DateTime? DateArrive { get; set; }
    public Chambre? Chambre { get; set; }
    public TypeReservation TypeReservation { get; set; } = default!;
    public Statut StatutReservation { get; set; }
}
