namespace FSH.WebApi.Domain.Ize;
public class TypeChambre : AuditableEntity, IAggregateRoot
{
    public string Code { get; private set; } = default!;
    public string Libelle { get; private set; } = default!;

    public TypeChambre(string code, string libelle)
    {
        Code = code;
        Libelle = libelle;
    }

    public TypeChambre Update(string? code, string? libelle)
    {
        if (code is not null && Code?.Equals(code) is not true)
            Code = code;
        if (libelle is not null && Libelle?.Equals(libelle) is not true)
            Libelle = libelle;
        return this;
    }
}
