using FSH.WebApi.Application.Catalog.Products;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class VenteProduitDto : IDto
{
    public int Quantite { get; set; }
    public decimal Prix { get; set; }
    public Guid ProductId { get; set; }
    public ProductDto Product { get; set; }
    public Guid VenteId { get; set; }
}
