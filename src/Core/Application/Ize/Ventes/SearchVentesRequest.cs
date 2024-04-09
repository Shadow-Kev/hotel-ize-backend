using FSH.WebApi.Application.Ize.Chambres;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class SearchVentesRequest : PaginationFilter, IRequest<PaginationResponse<VenteDto>>
{
    public Guid ProductId { get; set; }
    public Guid AgentId { get; set; }
    public decimal Prix { get; set; }
    public int Quantite { get; set; }
}

public class SearchVentesRequestHandler : IRequestHandler<SearchVentesRequest, PaginationResponse<VenteDto>>
{
    private readonly IReadRepository<Vente> _repository;
    private readonly IReadRepository<Product> _productRepository;
    private readonly IReadRepository<Agent> _agentRepository;

    public SearchVentesRequestHandler(IReadRepository<Vente> repository, IReadRepository<Product> productRepository, IReadRepository<Agent> agentRepository) => (_repository, _productRepository, _agentRepository) = (repository, productRepository, agentRepository);
    public async Task<PaginationResponse<VenteDto>> Handle(SearchVentesRequest request, CancellationToken cancellationToken)
    {
        var spec = new VenteBySearchRequestWithProduitSpec(request);
        var ventes = await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
        var ventesDto = new List<VenteDto>();

        foreach (var c in ventes.Data)
        {
            var product = await _productRepository.GetByIdAsync(c.ProductId);
            string productNom = product != null ? product.Name : "N/A";

            ventesDto.Add(new VenteDto
            {
                Id = c.Id,
                Quantite = c.Quantite,
                AgentNom = c.AgentNom,
                AgentId = c.AgentId,
                ProductNom = c.ProductNom,
                ProductId = c.ProductId,
            });
        }

        return new PaginationResponse<VenteDto>(ventesDto, ventesDto.Count, ventes.CurrentPage, ventes.PageSize)
        {
            Data = ventesDto,
            CurrentPage = ventes.CurrentPage,
            PageSize = ventes.PageSize,
            TotalPages = (int)Math.Ceiling(ventesDto.Count / (double)ventes.PageSize),
            TotalCount = ventesDto.Count
        };
    }
}
