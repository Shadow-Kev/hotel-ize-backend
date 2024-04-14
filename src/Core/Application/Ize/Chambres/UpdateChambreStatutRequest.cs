
using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class UpdateChambreStatutRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public bool Disponible { get; set; }
}

public class UpdateChambreStatutRequestHandler : IRequestHandler<UpdateChambreStatutRequest, Guid>
{
    private readonly IRepository<Chambre> _repository;
    private readonly IStringLocalizer _localizer;

    public UpdateChambreStatutRequestHandler(IRepository<Chambre> repository,IStringLocalizer<UpdateChambreRequestHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task<Guid> Handle(UpdateChambreStatutRequest request, CancellationToken cancellationToken)
    {
        var chambre = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = chambre ?? throw new NotFoundException(_localizer["Chambre {0} non trouvé", request.Id]);

        var updatedStatutChambre = chambre.Update(request.Disponible);
        chambre.DomainEvents.Add(EntityUpdatedEvent.WithEntity(chambre));
        await _repository.UpdateAsync(updatedStatutChambre, cancellationToken);

        return request.Id;
    }
}
