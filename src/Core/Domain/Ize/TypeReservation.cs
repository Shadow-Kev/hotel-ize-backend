namespace FSH.WebApi.Domain.Ize;
public class TypeReservation : AuditableEntity, IAggregateRoot
{
    public string Libelle { get; private set; } = default!;

    public TypeReservation(string libelle)
    {
        Libelle = libelle;
    }

    public TypeReservation Update( string? libelle)
    {
        if (libelle is not null && Libelle?.Equals(libelle) is not true)
            Libelle = libelle;
        return this;
    }
}
