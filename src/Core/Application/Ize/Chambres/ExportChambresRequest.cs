using FSH.WebApi.Application.Common.Exporters;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class ExportChambresRequest : BaseFilter, IRequest<Stream>
{
    public Guid? TypeChambreId { get; set; }
    public int? Capacite {  get; set; }
    public decimal? Prix { get; set; }
}

public class ExportChambresRequestHandler : IRequestHandler<ExportChambresRequest, Stream>
{
    private readonly IReadRepository<Chambre> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportChambresRequestHandler(IReadRepository<Chambre> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportChambresRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportChambresWithTypeChambresSpecification(request);
        var list = await _repository.ListAsync(spec);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportChambresWithTypeChambresSpecification : EntitiesByBaseFilterSpec<Chambre, ChambreExportDto>
{
    public ExportChambresWithTypeChambresSpecification(ExportChambresRequest request)
        : base(request) =>
        Query
            .Include(c => c.TypeChambre)
            .Where(c => c.TypeChambreId.Equals(request.TypeChambreId!.Value), request.TypeChambreId.HasValue);
}
