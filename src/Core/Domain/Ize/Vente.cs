using FSH.WebApi.Domain.Catalog;

namespace FSH.WebApi.Domain.Ize;
public class Vente : AuditableEntity, IAggregateRoot
{
    public int Quantite { get; set; }
    public Guid ProductId { get; private set; }
    public virtual Product Product { get; private set; } = default!;
    public Guid AgentId { get; set; }
    public virtual Agent Agent { get; set; }


    public Vente() { }

    public Vente(int quantite, Guid productId, Guid agentId)
    {
        Quantite = quantite;
        ProductId = productId;
        AgentId = agentId;
    }

    public Vente Update(int quantite, Guid productId, Guid agentId)
    {
        if (Quantite != 0 && Quantite != quantite) Quantite = quantite;
        if (ProductId != productId && ProductId != productId) ProductId = productId;
        if (AgentId != agentId && AgentId != agentId) AgentId = agentId;
        return this;
    }

}