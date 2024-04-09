using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class VenteBySearchRequestWithProduitSpec : EntitiesByPaginationFilterSpec<Vente, VenteDto>
{
    public VenteBySearchRequestWithProduitSpec(SearchVentesRequest request)
         : base(request) =>
        Query
            .Include(c => c.Product)
            .Include(c => c.Agent)
            .OrderBy(c => c.Quantite, !request.HasOrderBy())
            .Where(c => c.ProductId.Equals(request.ProductId) & c.AgentId.Equals(request.AgentId));   
}
