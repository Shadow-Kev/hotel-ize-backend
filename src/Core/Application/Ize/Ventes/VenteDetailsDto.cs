using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class VenteDetailsDto : IDto
{
    public Guid Id { get; set; }
    public Agent Agent { get; set; } = default!;
    public Client Client { get; set; } = default!;
    public ICollection<VenteProduitDto> VenteProduits { get; set; } = new List<VenteProduitDto>();
}
