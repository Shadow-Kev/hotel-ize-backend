using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSH.WebApi.Domain.Ize;
public class Client : AuditableEntity, IAggregateRoot
{
    public string Nom { get; private set; } = default!;
    public string? Prenom { get; private set; }
    public string? NomDeJeuneFille { get; private set; }
    public string? LieuDeNaissance { get; private set; }
    public string? Nationalite { get; private set; }
    public string? Profession { get; private set; }
    public string? Domicile { get; private set; }
    public string? MotifDuVoyage { get; private set; }
    public string? VenantDe { get; private set; }
    public string? AllantA { get; private set; }
    public DateTime? DateArrive { get; private set; }
    public DateTime? DateDepart { get; private set; }
    public string? Identite { get; private set; }
    public DateTime? DateIdentiteDelivreeLe { get; private set; }
    [DataType(DataType.PhoneNumber, ErrorMessage = "Veuillez saisir un numéro de téléphone valide")]
    [RegularExpression(@"^[9,7,2][0-9]{7}$", ErrorMessage = "Veuillez saisir un numéro de téléphone valide")]
    public string? Contact { get; private set; }
    public string? Email { get; private set; }
    public string? PersonneAPrevenir { get; private set; }
    [ForeignKey(nameof(AgentId))]
    public virtual Agent Agent { get; private set; } = default!;
    public Guid AgentId { get; private set; }
    [ForeignKey(nameof(ChambreId))]
    public virtual Chambre? Chambre { get; private set; }
    public Guid? ChambreId { get; private set; }
    public virtual ICollection<Vente>? Ventes { get; private set; } = new HashSet<Vente>();


    public Client(string nom, string? prenom, string? nomDeJeuneFille, string? lieuDeNaissance, string? nationalite, string? profession, string? domicile, string? motifDuVoyage, string? venantDe, string? allantA, DateTime? dateArrive, DateTime? dateDepart, string? identite, DateTime? dateIdentiteDelivreeLe, string? contact, string? email, string? personneAPrevenir, Guid agentId, Guid? chambreId)
    {
        Nom = nom;
        Prenom = prenom;
        NomDeJeuneFille = nomDeJeuneFille;
        LieuDeNaissance = lieuDeNaissance;
        Nationalite = nationalite;
        Profession = profession;
        Domicile = domicile;
        MotifDuVoyage = motifDuVoyage;
        VenantDe = venantDe;
        AllantA = allantA;
        DateArrive = dateArrive;
        DateDepart = dateDepart;
        Identite = identite;
        DateIdentiteDelivreeLe = dateIdentiteDelivreeLe;
        Contact = contact;
        Email = email;
        PersonneAPrevenir = personneAPrevenir;
        AgentId = agentId;
        ChambreId = chambreId;
    }

    public Client Update(string? nom, string? prenom, string? nomDeJeuneFille, string? lieuDeNaissance, string? nationalite, string? profession, string? domicile, string? motifDuVoyage, string? venantDe, string? allantA, DateTime? dateArrive, DateTime? dateDepart, string? identite, DateTime? dateIdentiteDelivreeLe, string? contact, string? email, string? personneAPrevenir, Guid? agentId, Guid? chambreId)
    {
        if (nom is not null && Nom.Equals(nom) is not true)
            Nom = nom;
        if (prenom is not null && Prenom?.Equals(prenom) is not true)
            Prenom = prenom;
        if (nomDeJeuneFille is not null && NomDeJeuneFille?.Equals(nomDeJeuneFille) is not true)
            NomDeJeuneFille = nomDeJeuneFille;
        if (lieuDeNaissance is not null && LieuDeNaissance?.Equals(lieuDeNaissance) is not true)
            LieuDeNaissance = lieuDeNaissance;
        if (nationalite is not null && Nationalite?.Equals(nationalite) is not true)
            Nationalite = nationalite;
        if (profession is not null && Profession?.Equals(profession) is not true)
            Profession = profession;
        if (domicile is not null && Domicile?.Equals(domicile) is not true)
            Domicile = domicile;
        if (motifDuVoyage is not null && MotifDuVoyage?.Equals(motifDuVoyage) is not true)
            MotifDuVoyage = motifDuVoyage;
        if (venantDe is not null && VenantDe?.Equals(venantDe) is not true)
            VenantDe = venantDe;
        if (allantA is not null && AllantA?.Equals(allantA) is not true)
            AllantA = allantA;
        if (dateArrive.HasValue && dateArrive != DateArrive)
            DateArrive = dateArrive.Value;
        if (dateDepart.HasValue && DateDepart != dateDepart)
            DateDepart = dateDepart.Value;
        if (identite is not null && Identite?.Equals(identite) is not true)
            Identite = identite;
        if (dateIdentiteDelivreeLe.HasValue && DateIdentiteDelivreeLe != dateIdentiteDelivreeLe)
            DateIdentiteDelivreeLe = dateIdentiteDelivreeLe.Value;
        if (contact is not null && Contact?.Equals(contact) is not true)
            Contact = contact;
        if (email is not null && Email?.Equals(email) is not true)
            Email = email;
        if (personneAPrevenir is not null && PersonneAPrevenir?.Equals(personneAPrevenir) is not true)
            PersonneAPrevenir = personneAPrevenir;
        if (agentId.HasValue && agentId.Value != Guid.Empty && !AgentId.Equals(agentId))
            AgentId = agentId.Value;
        if (chambreId.HasValue && chambreId.Value != Guid.Empty && !ChambreId.Equals(chambreId))
            ChambreId = chambreId.Value;
        return this;
    }
}
