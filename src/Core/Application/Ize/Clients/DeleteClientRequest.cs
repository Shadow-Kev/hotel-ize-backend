using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Clients;
public class DeleteClientRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteClientRequest(Guid id) => Id = id;
}

public class DeleteClientRequestHandler : IRequestHandler<DeleteClientRequest, Guid>
{
    private readonly IRepository<Client> _repository;
    private readonly IStringLocalizer _localizer;

    public DeleteClientRequestHandler(IRepository<Client> repository, IStringLocalizer<DeleteClientRequestHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }
    public async Task<Guid> Handle(DeleteClientRequest request, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = client ?? throw new NotFoundException(_localizer["Client {0} non trouvé", request.Id]);
        client.DomainEvents.Add(EntityDeletedEvent.WithEntity(client));
        await _repository.DeleteAsync(client, cancellationToken);
        return request.Id;
    }
}
