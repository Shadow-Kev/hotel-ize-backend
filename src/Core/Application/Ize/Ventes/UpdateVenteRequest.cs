
using FSH.WebApi.Application.Ize.Ventes;
using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class UpdateVenteRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public int Quantite { get; set; }
    public Guid ProductId { get; private set; }
    public Guid AgentId { get; set; }
}

public class UpdateVenteRequestValidator : IRequestHandler<UpdateVenteRequest, Guid>
{
    private readonly IRepository<Vente> _repository;
    private readonly IStringLocalizer _localizer;

    public UpdateVenteRequestValidator(IRepository<Vente> repository, IStringLocalizer<UpdateVenteRequestValidator> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }
    public async Task<DefaultIdType> Handle(UpdateVenteRequest request, CancellationToken cancellationToken)
    {
        var vente = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = vente ?? throw new NotFoundException(_localizer["Vente {0} non trouvé", request.Id]);

        var updatedVente = vente.Update(request.Quantite, request.ProductId, request.AgentId);
        vente.DomainEvents.Add(EntityUpdatedEvent.WithEntity(vente));
        await _repository.UpdateAsync(updatedVente, cancellationToken);

        return request.Id;
    }
}
