using FSH.WebApi.Application.Ize.TypeChambres;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class GetAllChambreRequest : IRequest<List<ChambreDto>>
{
    public GetAllChambreRequest() { }
}

public class GetAllChambreSpec : Specification<Chambre, ChambreDto>
{
}

public class GetAllChambreRequestHandler : IRequestHandler<GetAllChambreRequest, List<ChambreDto>>
{
    private readonly IRepository<Chambre> _repository;
    private readonly IStringLocalizer _t;
    public GetAllChambreRequestHandler(IRepository<Chambre> repository, IStringLocalizer<GetAllChambreRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }

    public async Task<List<ChambreDto>> Handle(GetAllChambreRequest request, CancellationToken cancellationToken) =>
        await _repository.ListAsync<ChambreDto>(new GetAllChambreSpec(), cancellationToken)
        ?? throw new NotFoundException(_t["Aucune chambre trouvé."]);
}
