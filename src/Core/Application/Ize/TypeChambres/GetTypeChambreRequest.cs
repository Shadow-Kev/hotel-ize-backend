using FSH.WebApi.Domain.Ize;

namespace FSH.WebApi.Application.Ize.TypeChambres;
public class GetTypeChambreRequest : IRequest<TypeChambreDto>
{
    public Guid Id { get; set; }

    public GetTypeChambreRequest(Guid id) => Id = id;
}

public class TypeChambreByIdSpec : Specification<TypeChambre, TypeChambreDto>, ISingleResultSpecification
{
    public TypeChambreByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetTypeChambreRequestHandler : IRequestHandler<GetTypeChambreRequest, TypeChambreDto>
{
    private readonly IRepository<TypeChambre> _repository;
    private readonly IStringLocalizer _t;

    public GetTypeChambreRequestHandler(IRepository<TypeChambre> repository, IStringLocalizer<GetTypeChambreRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;

    }

    public async Task<TypeChambreDto> Handle(GetTypeChambreRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<TypeChambre, TypeChambreDto>)new TypeChambreByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Type chambre {0} non trouvé.", request.Id]);
}