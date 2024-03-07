using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Agents;
public class DeleteAgentRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public DeleteAgentRequest(Guid id) => Id = id;
}

public class DeleteAgentRequestHandler : IRequestHandler<DeleteAgentRequest, Guid>
{
    private readonly IRepository<Agent> _repository;
    private readonly IStringLocalizer _localizer;

    public DeleteAgentRequestHandler(IRepository<Agent> repository, IStringLocalizer<DeleteAgentRequestHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task<Guid> Handle(DeleteAgentRequest request, CancellationToken cancellationToken)
    {
        var agent = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = agent ?? throw new NotFoundException(_localizer["Agent {0} non trouvé", request.Id]);
        agent.DomainEvents.Add(EntityDeletedEvent.WithEntity(agent));
        await _repository.DeleteAsync(agent, cancellationToken);
        return request.Id;
    }
}
