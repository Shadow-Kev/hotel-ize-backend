using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class UpdateChambreRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = default!;
    public int Capacite { get; set; }
    public decimal Prix { get; set; }
    public string? ImagePath { get; set; }
    public bool Disponible { get; set; }
    public bool Climatisee { get; set; }
    public bool PetitDejeunerInclus { get; set; }
    public Guid TypeChambreId { get; set; }
}

public class UpdateChambreRequestHandler : IRequestHandler<UpdateChambreRequest, Guid>
{
    private readonly IRepository<Chambre> _repository;
    private readonly IStringLocalizer _localizer;

    public UpdateChambreRequestHandler(IRepository<Chambre> repository, IStringLocalizer<UpdateChambreRequestHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }
    public async Task<Guid> Handle(UpdateChambreRequest request, CancellationToken cancellationToken)
    {
        var chambre = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = chambre ?? throw new NotFoundException(_localizer["Chambre {0} non trouvé", request.Id]);
        var updatedChambre = chambre.Update(request.Nom, request.Capacite, request.Prix, request.ImagePath, request.Disponible, request.Climatisee, request.PetitDejeunerInclus, request.TypeChambreId);
        chambre.DomainEvents.Add(EntityUpdatedEvent.WithEntity(chambre));
        await _repository.UpdateAsync(updatedChambre, cancellationToken);

        return request.Id;
    }
}
