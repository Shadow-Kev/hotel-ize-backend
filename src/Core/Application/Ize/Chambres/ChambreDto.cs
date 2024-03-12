using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class ChambreDto : IDto
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = default!;
    public int Capacite { get; set; }
    public decimal Prix { get; set; }
    public string? ImagePath { get; set; }
    public bool Disponible { get; set; }
    public bool Climatisee { get; set; }
    public bool PetitDejeunerInclus { get; set; }
    public Guid TypeChambreId { get; set; }
    public virtual ICollection<Client>? Clients { get; set; } = new HashSet<Client>();
}
