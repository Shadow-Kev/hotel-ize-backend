using System.ComponentModel.DataAnnotations.Schema;

namespace FSH.WebApi.Domain.Ize;
public class Reservation : AuditableEntity, IAggregateRoot
{
    public string Nom { get; private set; } = default!;
    public string Prenom { get; private set; } = default!;
    [ForeignKey(nameof(TypeReservationId))]
    public virtual TypeReservation TypeReservation { get; private set; } = default!;
    public Guid TypeReservationId { get; private set; }
    public DateTime? DateResevation { get; private set; }

}
