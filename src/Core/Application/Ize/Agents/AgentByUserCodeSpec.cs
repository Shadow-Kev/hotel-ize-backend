using FSH.WebApi.Domain.Ize;
namespace FSH.WebApi.Application.Ize.Agents;
public class AgentByUserCodeSpec : Specification<Agent>, ISingleResultSpecification
{
    public AgentByUserCodeSpec(Guid userCode) =>
        Query.Where(a => a.UserCode == userCode);
}
