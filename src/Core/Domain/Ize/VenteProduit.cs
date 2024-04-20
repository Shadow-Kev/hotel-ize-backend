using FSH.WebApi.Domain.Catalog;

namespace FSH.WebApi.Domain.Ize;
public class VenteProduit : AuditableEntity
{
    public int Quantite { get; set; }
    public decimal Prix { get; set; }
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; } = default!;
    public Guid VenteId { get; set; }
    public virtual Vente Vente { get; set; } = default!;

    public VenteProduit(Product product, int quantite, decimal prix)
    {
        Product = product;
        Quantite = quantite;
        Prix = prix;
    }
}
