using FSH.WebApi.Application.Ize.TypeChambres;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Agents;
public class GetAgentRequest : IRequest<AgentDto>
{
    public Guid Id { get; set; }

    public GetAgentRequest(Guid id) => Id = id;
}

public class AgentByIdSpec : Specification<Agent, AgentDto>, ISingleResultSpecification
{
    public AgentByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetAgentRequestHandler : IRequestHandler<GetAgentRequest, AgentDto>
{
    private readonly IRepository<Agent> _repository;
    private readonly IStringLocalizer _t;

    public GetAgentRequestHandler(IRepository<Agent> repository, IStringLocalizer<GetAgentRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;

    }

    public async Task<AgentDto> Handle(GetAgentRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Agent, AgentDto>)new AgentByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Agent {0} non trouvé.", request.Id]);
}