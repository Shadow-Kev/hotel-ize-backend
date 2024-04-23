using FSH.WebApi.Domain.Catalog;
using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Ize;
using System.Net;
using System.Text;
using System;
namespace FSH.WebApi.Application.Ize.Ventes;
public class CreateVenteRequest : IRequest<Guid>
{
    public Guid AgentId { get; set; }
    public List<ProductQuantite> Products { get; set; }

    public class ProductQuantite
    {
        public Guid ProductId { get; set; }
        public int Quantite { get; set; }
    }
}

public class CreateVenteRequestHandler : IRequestHandler<CreateVenteRequest, Guid>
{
    private readonly IRepository<Vente> _repository;
    private readonly IReadRepository<Product> _productRepository;
    public CreateVenteRequestHandler(IRepository<Vente> repository, IReadRepository<Product> productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }
    public async Task<Guid> Handle(CreateVenteRequest request, CancellationToken cancellationToken)
    {
        var vente = new Vente(request.AgentId);
        foreach (var pq in request.Products)
        {
            var product = await _productRepository.GetByIdAsync(pq.ProductId);
            if (product is not null)
                vente.AddProduct(product, pq.Quantite);
            else throw new Exception($"Le produit avec l'ID {pq.ProductId} n'existe pas");
        }

        vente.DomainEvents.Add(EntityCreatedEvent.WithEntity(vente));
        await _repository.AddAsync(vente, cancellationToken);
        return vente.Id;
    }
}