using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class SearchVentesRequest : PaginationFilter, IRequest<PaginationResponse<VenteDto>>
{
    public Guid? AgentId { get; set; }
}

public class SearchVentesRequestHandler : IRequestHandler<SearchVentesRequest, PaginationResponse<VenteDto>>
{
    private readonly IReadRepository<Vente> _repository;

    public SearchVentesRequestHandler(IReadRepository<Vente> repository) => _repository = repository;
    public async Task<PaginationResponse<VenteDto>> Handle(SearchVentesRequest request, CancellationToken cancellationToken)
    {
        var spec = new VenteBySearchRequestWithProduitSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
