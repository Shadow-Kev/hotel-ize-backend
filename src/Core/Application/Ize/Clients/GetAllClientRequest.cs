using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Clients;
public class GetAllClientRequest : IRequest<List<ClientDetailsDto>>
{
    public GetAllClientRequest() { }
}

public class GetAllClientSpec : Specification<Client, ClientDetailsDto>
{
    public GetAllClientSpec() =>
        Query.Include(c => c.Agent)
        .Include(c => c.Chambre);
}

public class GetAllClientRequestHandler : IRequestHandler<GetAllClientRequest, List<ClientDetailsDto>>
{
    private readonly IRepository<Client> _repository;
    private readonly IStringLocalizer _t;
    public GetAllClientRequestHandler(IRepository<Client> repository, IStringLocalizer<GetAllClientRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }

    public async Task<List<ClientDetailsDto>> Handle(GetAllClientRequest request, CancellationToken cancellationToken) =>
        await _repository.ListAsync<ClientDetailsDto>(new GetAllClientSpec(), cancellationToken)
        ?? throw new NotFoundException(_t["Aucun client trouvé."]);
}
