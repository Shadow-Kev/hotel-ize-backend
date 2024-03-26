using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Agents;

public class UpdateAgentRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public Guid UserCode { get; set; }
    public string? Prenoms { get; set; }
    public string? Nom { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateAgentRequestHandler : IRequestHandler<UpdateAgentRequest, Guid>
{
    private readonly IRepository<Agent> _repository;
    private readonly IStringLocalizer _localizer;

    public UpdateAgentRequestHandler(IRepository<Agent> repository, IStringLocalizer<UpdateAgentRequestHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task<Guid> Handle(UpdateAgentRequest request, CancellationToken cancellationToken)
    {
        var agent = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = agent ?? throw new NotFoundException(_localizer["Agent {0} non trouvé", request.Id]);
        var updatedAgent = agent.Update(request.UserCode, request.Prenoms, request.Nom, request.IsActive);
        agent.DomainEvents.Add(EntityUpdatedEvent.WithEntity(agent));
        await _repository.UpdateAsync(updatedAgent, cancellationToken);
        return request.Id;
    }
}
