using FSH.WebApi.Application.Catalog.Products;
using FSH.WebApi.Application.Common.Exporters;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Clients;
public class ExportClientsRequest : BaseFilter, IRequest<Stream>
{
    public Guid? AgentId { get; set; }
    public Guid? ChambreId { get; set; }
}

public class ExportClientsRequestHandler : IRequestHandler<ExportClientsRequest, Stream>
{
    private readonly IReadRepository<Client> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportClientsRequestHandler(IReadRepository<Client> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportClientsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportClientsWithAgentandChambreSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportClientsWithAgentandChambreSpecification : EntitiesByBaseFilterSpec<Client, ClientExportDTO>
{
    public ExportClientsWithAgentandChambreSpecification(ExportClientsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Agent)
            .Include(p => p.Chambre)
            .Where(p => p.AgentId.Equals(request.AgentId!.Value), request.AgentId.HasValue)
            .Where(p => p.ChambreId >= request.ChambreId!.Value, request.ChambreId.HasValue);
}