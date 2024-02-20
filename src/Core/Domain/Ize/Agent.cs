namespace FSH.WebApi.Domain.Ize;
public class Agent : AuditableEntity, IAggregateRoot
{
    public Guid UserCode { get; set; }
    public string? Prenoms { get; private set; }
    public string? Nom { get; private set; }
    public bool IsActive { get; private set; }

    public Agent(Guid userCode, string? prenoms, string? nom, bool isActive)
    {
        UserCode = userCode;
        Prenoms = prenoms;
        Nom = nom;
        IsActive = isActive;
    }

    public Agent Update(Guid? userCode, string? prenoms, string? nom, bool? isActive)
    {
        if (userCode.HasValue && userCode.Value != Guid.Empty && !UserCode.Equals(userCode))
            UserCode = userCode.Value;
        if (prenoms is not null && !Prenoms.Equals(prenoms))
            Prenoms = prenoms;
        if (nom is not null && !Nom.Equals(nom))
            Nom = nom;
        if (isActive is not null && !Nom.Equals(nom))
            IsActive = isActive.Value;
        return this;
    }
}
