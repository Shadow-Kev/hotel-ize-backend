using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeChambres;
public class DeleteTypeChambreRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteTypeChambreRequest(Guid id) => Id = id;
}

public class DeleteTypeChambreRequestHandler : IRequestHandler<DeleteTypeChambreRequest, Guid>
{
    private readonly IRepository<TypeChambre> _repository;
    private readonly IStringLocalizer _localizer;

    public DeleteTypeChambreRequestHandler(IRepository<TypeChambre> repository, IStringLocalizer<DeleteTypeChambreRequestHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task<Guid> Handle(DeleteTypeChambreRequest request, CancellationToken cancellationToken)
    {
        var typeChambre = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = typeChambre ?? throw new NotFoundException(_localizer["Type chambre {0} non trouvé", request.Id]);
        typeChambre.DomainEvents.Add(EntityDeletedEvent.WithEntity(typeChambre));
        await _repository.DeleteAsync(typeChambre, cancellationToken);
        return request.Id;
    }
}
