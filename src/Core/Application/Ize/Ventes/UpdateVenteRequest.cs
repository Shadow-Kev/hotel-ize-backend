using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class UpdateVenteRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public Guid AgentId { get; set; }
    public Guid ClientId { get; set; }
    public List<ProductQuantite> Products { get; set; }

    public class ProductQuantite
    {
        public Guid ProductId { get; set; }
        public int Quantite { get; set; }
    }
}

public class UpdateVenteRequestValidator : IRequestHandler<UpdateVenteRequest, Guid>
{
    private readonly IRepository<Vente> _repository;
    private readonly IReadRepository<Product> _productRepository;
    private readonly IStringLocalizer _localizer;

    public UpdateVenteRequestValidator(IRepository<Vente> repository, IReadRepository<Product> productRepository, IStringLocalizer<UpdateVenteRequestValidator> localizer)
    {
        _repository = repository;
        _productRepository = productRepository;
        _localizer = localizer;
    }
    public async Task<DefaultIdType> Handle(UpdateVenteRequest request, CancellationToken cancellationToken)
    {
        var vente = await _repository.GetByIdAsync(request.Id, cancellationToken);
        _ = vente ?? throw new NotFoundException(_localizer["Vente {0} non trouvé", request.Id]);

        var updatedVente = vente.Update(request.AgentId,request.ClientId);
        if (request.Products is not null)
        {
            updatedVente.VenteProduits.Clear();
            foreach (var pq in request.Products)
            {
                var product = await _productRepository.GetByIdAsync(pq.ProductId);
                if (product is not null)
                    updatedVente.AddProduct(product, pq.Quantite!);
                else throw new Exception("{0} n'existe pas");
            }
        }

        vente.DomainEvents.Add(EntityUpdatedEvent.WithEntity(vente));
        await _repository.UpdateAsync(updatedVente, cancellationToken);

        return request.Id;
    }
}
