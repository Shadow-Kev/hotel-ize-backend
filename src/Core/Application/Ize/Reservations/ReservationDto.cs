using FSH.WebApi.Domain.Enums;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Reservations;
public class ReservationDto : IDto
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = default!;
    public string Prenom { get; set; } = default!;
    public string? ChambreNom { get; set; }
    public DateTime? DateArrive { get; set; }
    public Guid? ChambreId { get; set; }
    public Statut StatutReservation { get;  set; }
    public string TypeReservationLibelle { get; set; } = default!;
    public Guid TypeReservationId { get; set; }
}
