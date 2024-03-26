using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class DeleteChambreRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteChambreRequest(Guid id) => Id = id;
}

public class DeleteChambreRequestHandler : IRequestHandler<DeleteChambreRequest, Guid>
{
    private readonly IRepository<Chambre> _repository;
    private readonly IStringLocalizer _localizer;

    public DeleteChambreRequestHandler(IRepository<Chambre> repository, IStringLocalizer<DeleteChambreRequestHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task<Guid> Handle(DeleteChambreRequest request, CancellationToken cancellationToken)
    {
        var chambre = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = chambre ?? throw new NotFoundException(_localizer["Chambre {0} non trouvé", request.Id]);
        chambre.DomainEvents.Add(EntityDeletedEvent.WithEntity(chambre));
        await _repository.DeleteAsync(chambre, cancellationToken);
        return request.Id;
    }
}