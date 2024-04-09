using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Clients;
public class ClientsBySearchRequestWithChambreAndAgent : EntitiesByPaginationFilterSpec<Client, ClientDto>
{
    public ClientsBySearchRequestWithChambreAndAgent(SearchClientsRequest request)
        : base(request) =>
        Query
            .Include(c => c.Agent)
            .Include(c => c.Chambre)
            .ThenInclude(c => c.TypeChambre)
            .OrderBy(c => c.Nom, !request.HasOrderBy())
            .Where(c => c.ChambreId.Equals(request.ChambreId!.Value), request.ChambreId.HasValue)
            .Where(c => c.AgentId.Equals(request.AgentId!.Value), request.AgentId.HasValue);
}
