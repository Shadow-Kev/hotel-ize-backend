using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class GetAllVentesRequest : IRequest<List<VenteDetailsDto>>
{
    public GetAllVentesRequest() { }
}

public class GetAllVenteSpec : Specification<Vente, VenteDetailsDto>
{
    public GetAllVenteSpec() =>
        Query.Include(v => v.Agent);
}

public class GetAllVentesRequestHandler : IRequestHandler<GetAllVentesRequest, List<VenteDetailsDto>>
{
    private readonly IRepository<Vente> _repository;
    private readonly IStringLocalizer _t;
    public GetAllVentesRequestHandler(IRepository<Vente> repository, IStringLocalizer<GetAllVentesRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }
    public async Task<List<VenteDetailsDto>> Handle(GetAllVentesRequest request, CancellationToken cancellationToken) =>
        await _repository.ListAsync<VenteDetailsDto>(new GetAllVenteSpec(), cancellationToken)
        ?? throw new NotFoundException(_t["Aucune Ventes trouvé."]);
}
