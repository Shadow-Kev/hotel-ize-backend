using FSH.WebApi.Domain.Catalog;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSH.WebApi.Domain.Ize;
public class Vente : AuditableEntity, IAggregateRoot
{
    public Guid AgentId { get; set; }
    public virtual Agent Agent { get; set; }
    public Guid ClientId { get; private set; }
    public virtual Client Client { get; private set; }
    public virtual ICollection<VenteProduit> VenteProduits { get; set; } = new List<VenteProduit>();

    public Vente() { }

    public Vente(Guid agentId, Guid clientId)
    {
        AgentId = agentId;
        ClientId = clientId;
    }

    public Vente Update(Guid agentId, Guid clientId)
    {
        if (AgentId != agentId && ClientId != clientId)
        {
            AgentId = agentId;
            ClientId = clientId;
        }
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