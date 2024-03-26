using FSH.WebApi.Application.Ize.TypeChambres;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Agents;
public class GetAllAgentRequest : IRequest<List<AgentDto>>
{
    public GetAllAgentRequest() {}
}

public class GetAllAgentSpec : Specification<Agent, AgentDto>
{
}

public class GetAllAgentRequestRequestHandler : IRequestHandler<GetAllAgentRequest, List<AgentDto>>
{
    private readonly IRepository<Agent> _repository;
    private readonly IStringLocalizer _t;
    public GetAllAgentRequestRequestHandler(IRepository<Agent> repository, IStringLocalizer<GetAllAgentRequestRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }

    public async Task<List<AgentDto>> Handle(GetAllAgentRequest request, CancellationToken cancellationToken) =>
        await _repository.ListAsync<AgentDto>(new GetAllAgentSpec(), cancellationToken)
        ?? throw new NotFoundException(_t["Aucun agent trouvé."]);
}
