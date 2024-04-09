
using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class CreateVenteRequest : IRequest<Guid>
{
    public int Quantite { get; set; } = default!;
    public Guid ProductId { get; set; }
    public Guid AgentId { get; set; }
}


public class CreateVenteREquestHandler : IRequestHandler<CreateVenteRequest, Guid>
{
    private readonly IRepository<Vente> _repository;

    public CreateVenteREquestHandler(IRepository<Vente> repository)
    {
        _repository = repository;
    }
    public async Task<Guid> Handle(CreateVenteRequest request, CancellationToken cancellationToken)
    {
        var vente = new Vente(request.Quantite, request.ProductId, request.AgentId);

        vente.DomainEvents.Add(EntityCreatedEvent.WithEntity(vente));

        await _repository.AddAsync(vente, cancellationToken);
        return vente.Id;
    }
}
