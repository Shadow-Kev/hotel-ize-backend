using FSH.WebApi.Application.Ize.Chambres;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Clients;
public class ClientByIdWithAgentAndChambreSpec : Specification<Client, ClientDetailsDto>, ISingleResultSpecification
{
    public ClientByIdWithAgentAndChambreSpec(Guid id) =>
       Query
           .Where(c => c.Id == id)
           .Include(c => c.Chambre)
           .Include(c => c.Agent);
}
