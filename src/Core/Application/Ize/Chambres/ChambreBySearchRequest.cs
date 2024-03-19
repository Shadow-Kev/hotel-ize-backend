using FSH.WebApi.Application.Ize.TypeChambres;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class ChambreBySearchRequest : PaginationFilter, IRequest<PaginationResponse<ChambreDto>>
{
    public Guid? TypeChambreId { get; set; }
    public decimal? Prix { get; set; }
}

public class ChambreBySearchRequestHandler : IRequestHandler<ChambreBySearchRequest, PaginationResponse<ChambreDto>>
{
    private readonly IReadRepository<Chambre> _repository;
    public ChambreBySearchRequestHandler(IReadRepository<Chambre> repository)
    {
        _repository = repository;
    }

    public async Task<PaginationResponse<ChambreDto>> Handle(ChambreBySearchRequest request, CancellationToken cancellationToken)
    {
        var spec = new ChambreBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
