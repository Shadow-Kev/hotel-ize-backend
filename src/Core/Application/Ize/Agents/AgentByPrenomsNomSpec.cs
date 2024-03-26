using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Agents;
public class AgentByPrenomsNomSpec : Specification<Agent>, ISingleResultSpecification
{
    public AgentByPrenomsNomSpec(string prenoms, string nom) =>
        Query.Where(a => a.Prenoms == prenoms && a.Nom == nom);
}
