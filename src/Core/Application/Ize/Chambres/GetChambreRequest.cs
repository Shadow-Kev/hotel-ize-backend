using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class GetChambreRequest : IRequest<ChambreDetailsDto>
{
    public Guid Id { get; set; }

    public GetChambreRequest(Guid id) => Id = id;
}

public class GetChambreRequestHandler : IRequestHandler<GetChambreRequest, ChambreDetailsDto>
{
    private readonly IRepository<Chambre> _repository;
    private readonly IStringLocalizer _t;

    public GetChambreRequestHandler(IRepository<Chambre> repository, IStringLocalizer<GetChambreRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }

    public async Task<ChambreDetailsDto> Handle(GetChambreRequest request, CancellationToken cancellationToken) =>
     await _repository.FirstOrDefaultAsync(
           (ISpecification<Chambre, ChambreDetailsDto>)new ChambreByIdWithTypeChambreSpec(request.Id), cancellationToken)
       ?? throw new NotFoundException(_t["Chambre {0} non trouvé.", request.Id]);
}