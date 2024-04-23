using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class VenteDto : IDto
{
    public Guid Id { get; set; }
    public Guid AgentId { get; set; }
    public string AgentNom { get; set; }
    public ICollection<VenteProduitDto> VenteProduits { get; set; } = new List<VenteProduitDto>();
}
