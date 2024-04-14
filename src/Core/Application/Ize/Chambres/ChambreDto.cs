using FSH.WebApi.Application.Ize.Clients;
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
    public string TypeChambreNom { get; set; } = default!;
    public virtual ICollection<ClientDto>? Clients { get; set; } = new HashSet<ClientDto>();
}
