using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeChambres;
public class GetAllTypeChambreRequest : IRequest<List<TypeChambreDto>>
{
    public GetAllTypeChambreRequest() {}
}

public class GetAllTypeChambreSpec : Specification<TypeChambre, TypeChambreDto>
{
}

public class GetAllTypeChambreRequestHandler : IRequestHandler<GetAllTypeChambreRequest, List<TypeChambreDto>>
{
    private readonly IRepository<TypeChambre> _repository;
    private readonly IStringLocalizer _t;
    public GetAllTypeChambreRequestHandler(IRepository<TypeChambre> repository, IStringLocalizer<GetAllTypeChambreRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }

    public async Task<List<TypeChambreDto>> Handle(GetAllTypeChambreRequest request, CancellationToken cancellationToken) =>
        await _repository.ListAsync<TypeChambreDto>(new GetAllTypeChambreSpec(), cancellationToken)
        ?? throw new NotFoundException(_t["Aucun type de chambre trouvé."]);
}
