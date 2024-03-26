using FSH.WebApi.Application.Ize.Chambres;
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
    private readonly IRepositoryWithEvents<TypeChambre> _typeChambreRepo;
    private readonly IRepository<Chambre> _chambreRepo;
    private readonly IStringLocalizer _localizer;

    public DeleteTypeChambreRequestHandler(IRepositoryWithEvents<TypeChambre> typeChambreRepo, IRepository<Chambre> chambreRepo, IStringLocalizer<DeleteTypeChambreRequestHandler> localizer)
    {
        _typeChambreRepo = typeChambreRepo;
        _chambreRepo = chambreRepo;
        _localizer = localizer;
    }

    public async Task<Guid> Handle(DeleteTypeChambreRequest request, CancellationToken cancellationToken)
    {
        if (await _chambreRepo.AnyAsync(new ChambresByTypeChambreSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_localizer["TypeChambre ne peut pas être supprimé"]);
        }

        var typeChambre = await _typeChambreRepo.GetByIdAsync(request.Id, cancellationToken);
        _ = typeChambre ?? throw new NotFoundException(_localizer["Type chambre {0} non trouvé", request.Id]);
        typeChambre.DomainEvents.Add(EntityDeletedEvent.WithEntity(typeChambre));
        await _typeChambreRepo.DeleteAsync(typeChambre, cancellationToken);
        return request.Id;
    }
}
