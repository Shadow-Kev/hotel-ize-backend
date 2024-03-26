using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Agents;
public class AgentBySearchRequestSpec : EntitiesByPaginationFilterSpec<Agent, AgentDto>
{
    public AgentBySearchRequestSpec(AgentBySearchRequest request)
        : base(request)
        => Query.OrderBy(a => a.Prenoms, !request.HasOrderBy());
}
