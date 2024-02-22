using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSH.WebApi.Domain.Ize;
public class Chambre : AuditableEntity, IAggregateRoot
{
    public string Nom { get; private set; } = default!;
    [Range(0, int.MaxValue)]
    public int Capacite { get; private set; }
    [Range(0, double.PositiveInfinity)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Prix { get; private set; }
    public string? ImagePath { get; private set; }
    public bool Disponible { get; private set; }
    public bool Climatisee { get; private set; }
    public bool PetitDejeunerInclus { get; private set; }
    [ForeignKey(nameof(TypeChambreId))]
    public virtual TypeChambre TypeChambre { get; private set; } = default!;
    public Guid TypeChambreId { get; private set; }
    public virtual ICollection<Client>? Clients { get; private set; } = new HashSet<Client>();

    public Chambre(int capacite, decimal prix, string? imagePath, bool disponible, bool climatisee, bool petitDejeunerInclus, Guid typeChambreId)
    {
        Capacite = capacite;
        Prix = prix;
        ImagePath = imagePath;
        Disponible = disponible;
        Climatisee = climatisee;
        PetitDejeunerInclus = petitDejeunerInclus;
        TypeChambreId = typeChambreId;
        Clients = new HashSet<Client>();
    }

    public Chambre Update(int? capacite, decimal? prix, string? imagePath, bool? disponible, bool? climatisee, bool? petitDejeunerInclus, Guid? typeChambreId, ICollection<Client>? clients)
    {
        if (capacite.HasValue && Capacite != capacite)
            Capacite = capacite.Value;
        if (prix.HasValue && Prix != prix)
            Prix = prix.Value;
        if (imagePath is not null && !ImagePath.Equals(imagePath))
            ImagePath = imagePath;
        if (disponible is not null && Disponible.Equals(disponible) is not true)
            Disponible = disponible.Value;
        if (climatisee is not null && Climatisee.Equals(climatisee) is not true)
            Climatisee = climatisee.Value;
        if (petitDejeunerInclus is not null && PetitDejeunerInclus.Equals(petitDejeunerInclus) is not true)
            PetitDejeunerInclus = petitDejeunerInclus.Value;
        if (typeChambreId.HasValue && typeChambreId.Value != Guid.Empty && !TypeChambreId.Equals(typeChambreId))
            TypeChambreId = typeChambreId.Value;
        if (clients is not null && clients.Any())
        {
            Clients.Clear();
            foreach (var client in clients)
                Clients.Add(client);
        }

        return this;
    }

    public Chambre ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}
