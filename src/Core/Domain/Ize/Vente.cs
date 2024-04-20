using FSH.WebApi.Domain.Catalog;

namespace FSH.WebApi.Domain.Ize;
public class Vente : AuditableEntity, IAggregateRoot
{
    public Guid AgentId { get; set; }
    public virtual Agent Agent { get; set; }
    public virtual ICollection<VenteProduit> VenteProduits { get; set; } = new List<VenteProduit>();

    public Vente() { }

    public Vente(Guid agentId)
    {
        AgentId = agentId;
    }

    public Vente Update(Guid agentId)
    {
        if (AgentId != agentId && AgentId != agentId) AgentId = agentId;
        return this;
    }

    public void AddProduct(Product product, int quantite)
    {
        if (product.Quantite < quantite)
        {
            throw new Exception("La quantité demandée est supérieure à la quantité disponible du produit");
        }

        product.Quantite -= quantite;
        VenteProduits.Add(new VenteProduit(product, quantite, product.Prix)
        {
            VenteId = this.Id
        });
    }

}