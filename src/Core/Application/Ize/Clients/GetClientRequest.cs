using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Clients;
public class GetClientRequest : IRequest<ClientDetailsDto>
{
    public Guid Id { get; set; }

    public GetClientRequest(Guid id) => Id = id;
}

public class GetClientRequestHandler : IRequestHandler<GetClientRequest, ClientDetailsDto>
{
    private readonly IRepository<Client> _repository;
    private readonly IStringLocalizer _t;
    public GetClientRequestHandler(IRepository<Client> repository, IStringLocalizer<GetAllClientRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }
    public async Task<ClientDetailsDto> Handle(GetClientRequest request, CancellationToken cancellationToken) =>
     await _repository.FirstOrDefaultAsync(
           (ISpecification<Client, ClientDetailsDto>)new ClientByIdWithAgentAndChambreSpec(request.Id), cancellationToken)
       ?? throw new NotFoundException(_t["Client {0} non trouvé.", request.Id]);
}