using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeChambres;
public class UpdateTypeChambreRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;

    public string Libelle { get; set; } = default!;
}

public class UpdateTypeChambreRequestHandler : IRequestHandler<UpdateTypeChambreRequest, Guid>
{
    private readonly IRepository<TypeChambre> _repository;
    private readonly IStringLocalizer _localizer;

    public UpdateTypeChambreRequestHandler(IRepository<TypeChambre> repository, IStringLocalizer<UpdateTypeChambreRequestHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task<Guid> Handle(UpdateTypeChambreRequest request, CancellationToken cancellationToken)
    {
        var typeChambre = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = typeChambre ?? throw new NotFoundException(_localizer["Type Chambre {0} non trouvé", request.Id]);
        var updatedTypeChambre = typeChambre.Update(request.Code, request.Libelle);
        typeChambre.DomainEvents.Add(EntityUpdatedEvent.WithEntity(typeChambre));
        await _repository.UpdateAsync(updatedTypeChambre, cancellationToken );

        return request.Id;
    }
}
