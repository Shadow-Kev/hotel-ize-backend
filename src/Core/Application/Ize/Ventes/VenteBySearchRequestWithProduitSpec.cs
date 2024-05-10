using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class VenteBySearchRequestWithProduitSpec : EntitiesByPaginationFilterSpec<Vente, VenteDto>
{
    public VenteBySearchRequestWithProduitSpec(SearchVentesRequest request)
         : base(request) =>
        Query
            .Include(v => v.Agent)
            .Where(v => v.AgentId.Equals(request.AgentId), request.AgentId.HasValue)
            .Include(v => v.Client)
            .Where(v => v.ClientId.Equals(request.ClientId), request.ClientId.HasValue);
}
