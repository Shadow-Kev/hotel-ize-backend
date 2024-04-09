using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Clients;
public class SearchClientsRequest : PaginationFilter, IRequest<PaginationResponse<ClientDto>>
{
    public Guid? ChambreId { get; set; }
    public Guid? AgentId { get; set; }
    public string? Nom { get; set; }
    public int? Contact { get; set; }
}

public class SearchClientsRequestHandler : IRequestHandler<SearchClientsRequest, PaginationResponse<ClientDto>>
{
    private readonly IReadRepository<Client> _repository;

    public SearchClientsRequestHandler(IReadRepository<Client> repository) => _repository = repository;

    public async Task<PaginationResponse<ClientDto>> Handle(SearchClientsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ClientsBySearchRequestWithChambreAndAgent(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
