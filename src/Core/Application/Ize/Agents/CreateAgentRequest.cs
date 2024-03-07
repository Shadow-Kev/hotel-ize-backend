using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Agents;
public class CreateAgentRequest : IRequest<Guid>
{
    public Guid UserCode { get; set; }
    public string? Prenoms { get;  set; }
    public string? Nom { get; set; }
    public bool IsActive { get;  set; }
}

public class CreateAgentRequestHandler : IRequestHandler<CreateAgentRequest, Guid>
{
    private readonly IRepository<Agent> _repository;

    public CreateAgentRequestHandler(IRepository<Agent> repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateAgentRequest request, CancellationToken cancellationToken)
    {
        var agent = new Agent(request.UserCode, request.Prenoms, request.Nom, request.IsActive);
        agent.DomainEvents.Add(EntityCreatedEvent.WithEntity(agent));
        await _repository.AddAsync(agent, cancellationToken);
        return agent.Id;
    }
}
