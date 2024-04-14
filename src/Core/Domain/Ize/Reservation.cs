using FSH.WebApi.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSH.WebApi.Domain.Ize;
public class Reservation : AuditableEntity, IAggregateRoot
{
    public string Nom { get; private set; } = default!;
    public string Prenom { get; private set; } = default!;
    [ForeignKey(nameof(ChambreId))]
    public virtual Chambre? Chambre { get; private set; } = default!;
    public Guid? ChambreId { get; private set; }
    public Statut StatutReservation { get; private set; }

    [ForeignKey(nameof(TypeReservationId))]
    public virtual TypeReservation TypeReservation { get; private set; } = default!;
    public Guid TypeReservationId { get; private set; }
    public DateTime? DateArrive { get; private set; }

    public Reservation(string nom, string prenom, Guid? chambreId, Guid typeReservationId, Statut statutReservation, DateTime? dateArrive)
    {
        Nom = nom;
        Prenom = prenom;
        ChambreId = chambreId;
        TypeReservationId = typeReservationId;
        StatutReservation = statutReservation;
        DateArrive = dateArrive;
    }

    public Reservation Update(string nom, string prenom, Guid? chambreId, Guid? typeReservationId, Statut statut, DateTime? dateArrive)
    {
        if (nom is not null && Nom.Equals(nom) is not true)
            Nom = nom;
        if (prenom is not null && Prenom.Equals(prenom) is not true)
            Prenom = prenom;
        if (chambreId.HasValue && chambreId.Value != Guid.Empty && !ChambreId.Equals(chambreId))
            ChambreId = chambreId.Value;
        if (typeReservationId.HasValue && typeReservationId.Value != Guid.Empty && !TypeReservationId.Equals(typeReservationId))
            TypeReservationId = typeReservationId.Value;
        if (statut != null && StatutReservation != statut)
            StatutReservation = statut;
        if (dateArrive.HasValue && dateArrive != DateArrive)
            DateArrive = dateArrive.Value;

        return this;
    }
}
