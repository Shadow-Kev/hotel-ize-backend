using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class DeleteVenteRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteVenteRequest(Guid id) => Id = id;
}

public class DeleteVenteRequestHandler : IRequestHandler<DeleteVenteRequest, Guid>
{
    private readonly IRepository<Vente> _repository;
    private readonly IStringLocalizer _localizer;

    public DeleteVenteRequestHandler(IRepository<Vente> repository, IStringLocalizer<DeleteVenteRequestHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task<DefaultIdType> Handle(DeleteVenteRequest request, CancellationToken cancellationToken)
    {
        var vente = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = vente ?? throw new NotFoundException(_localizer["Vente {0} non trouvé", request.Id]);
        vente.DomainEvents.Add(EntityDeletedEvent.WithEntity(vente));
        await _repository.DeleteAsync(vente, cancellationToken);
        return request.Id;
    }
}
