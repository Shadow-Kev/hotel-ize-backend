using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Agents;
public class AgentBySearchRequest : PaginationFilter, IRequest<PaginationResponse<AgentDto>>
{
}

public class AgentBySearchRequestHandler : IRequestHandler<AgentBySearchRequest, PaginationResponse<AgentDto>>
{
    private readonly IReadRepository<Agent> _repository;
    public AgentBySearchRequestHandler(IReadRepository<Agent> repository)
    {
        _repository = repository;
    }
    public async Task<PaginationResponse<AgentDto>> Handle(AgentBySearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new AgentBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
