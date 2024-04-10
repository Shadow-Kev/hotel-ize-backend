using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class VenteBySearchRequestWithProduitSpec : EntitiesByPaginationFilterSpec<Vente, VenteDto>
{
    public VenteBySearchRequestWithProduitSpec(SearchVentesRequest request)
         : base(request) =>
        Query
            .Include(v => v.Product)
            .Include(v => v.Agent)
            .OrderBy(v => v.Quantite, !request.HasOrderBy())
            .Where(v => v.ProductId.Equals(request.ProductId), request.ProductId.HasValue)
            .Where(v => v.AgentId.Equals(request.AgentId), request.AgentId.HasValue);
}
