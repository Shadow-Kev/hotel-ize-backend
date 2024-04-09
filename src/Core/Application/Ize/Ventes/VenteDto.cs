namespace FSH.WebApi.Application.Ize.Ventes;
public class VenteDto : IDto
{
    public Guid Id { get; set; }
    public int Quantite { get; set; }
    public Guid ProductId { get; set; }
    public string ProductNom { get; set; } = default!;
    public Guid AgentId { get; set; }
    public string AgentNom { get; set; }

}
