using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class VenteDetailsDto : IDto
{
    public Guid Id { get; set; }
    public int Quantite { get; set; }
    public Product Product{ get; set; } = default!;
    public Agent Agent { get; set; } = default!;
}
