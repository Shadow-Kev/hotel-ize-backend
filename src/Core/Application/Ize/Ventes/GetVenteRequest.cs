using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Ventes;
public class GetVenteRequest : IRequest<VenteDetailsDto>
{
    public Guid Id { get; set; }

    public GetVenteRequest(Guid id) => Id = id;
}

public class GetVenteRequestHandler : IRequestHandler<GetVenteRequest, VenteDetailsDto>
{
    private readonly IRepository<Vente> _repository;
    private readonly IStringLocalizer _t;

    public GetVenteRequestHandler(IRepository<Vente> repository, IStringLocalizer<GetVenteRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }

    public async Task<VenteDetailsDto> Handle(GetVenteRequest request, CancellationToken cancellationToken) =>
     await _repository.FirstOrDefaultAsync(
           (ISpecification<Vente, VenteDetailsDto>)new VenteByIdWithProduitSpec(request.Id), cancellationToken)
       ?? throw new NotFoundException(_t["Vente {0} non trouvé.", request.Id]);
}