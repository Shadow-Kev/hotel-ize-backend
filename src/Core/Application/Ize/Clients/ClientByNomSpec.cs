using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Clients;
public class ClientByNomSpec : Specification<Client>, ISingleResultSpecification
{
    public ClientByNomSpec(string nom) =>
        Query.Where(c => c.Nom == nom);

}
