using FSH.WebApi.Application.Ize.TypeChambres;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class GetAllChambreRequest : IRequest<List<ChambreDetailsDto>>
{
    public GetAllChambreRequest() { }
}

public class GetAllChambreSpec : Specification<Chambre, ChambreDetailsDto>
{
    public GetAllChambreSpec() =>
        Query.Include(c => c.TypeChambre);
}

public class GetAllChambreRequestHandler : IRequestHandler<GetAllChambreRequest, List<ChambreDetailsDto>>
{
    private readonly IRepository<Chambre> _repository;
    private readonly IStringLocalizer _t;
    public GetAllChambreRequestHandler(IRepository<Chambre> repository, IStringLocalizer<GetAllChambreRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }

    public async Task<List<ChambreDetailsDto>> Handle(GetAllChambreRequest request, CancellationToken cancellationToken) =>
        await _repository.ListAsync<ChambreDetailsDto>(new GetAllChambreSpec(), cancellationToken)
        ?? throw new NotFoundException(_t["Aucune chambre trouvé."]);
}
