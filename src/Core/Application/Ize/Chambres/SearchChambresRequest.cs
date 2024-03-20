using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class SearchChambresRequest : PaginationFilter, IRequest<PaginationResponse<ChambreDto>>
{
    public Guid? TypeChambreId { get; set; }
    public int? Capacite { get; set; }
    public decimal? Prix { get; set; }
}

public class SearchChambresRequestHandler : IRequestHandler<SearchChambresRequest, PaginationResponse<ChambreDto>>
{
    private readonly IReadRepository<Chambre> _repository;

    public SearchChambresRequestHandler(IReadRepository<Chambre> repository) => _repository = repository;

    public async Task<PaginationResponse<ChambreDto>> Handle(SearchChambresRequest request, CancellationToken cancellationToken)
    {
        var spec = new ChambreBySearchRequestWithTypeChambresSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
