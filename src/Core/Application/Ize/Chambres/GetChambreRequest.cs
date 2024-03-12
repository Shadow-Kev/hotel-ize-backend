using FSH.WebApi.Application.Ize.TypeChambres;
using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.Chambres;
public class GetChambreRequest : IRequest<ChambreDto>
{
    public Guid Id { get; set; }

    public GetChambreRequest(Guid id) => Id = id;

}
public class ChambreByIdSpec : Specification<Chambre, ChambreDto>, ISingleResultSpecification
{
    public ChambreByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}
public class GetChambreRequestHandler : IRequestHandler<GetChambreRequest, ChambreDto>
{
    private readonly IRepository<Chambre> _repository;
    private readonly IStringLocalizer _t;

    public GetChambreRequestHandler(IRepository<Chambre> repository, IStringLocalizer<GetChambreRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;

    }

    public async Task<ChambreDto> Handle(GetChambreRequest request, CancellationToken cancellationToken) =>
     await _repository.FirstOrDefaultAsync(
           (ISpecification<Chambre, ChambreDto>)new ChambreByIdSpec(request.Id), cancellationToken)
       ?? throw new NotFoundException(_t["Chambre {0} non trouvé.", request.Id]);
}